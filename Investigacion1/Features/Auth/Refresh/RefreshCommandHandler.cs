using Investigacion1.Features.Usuarios;
using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Auth.Refresh;

public static class RefreshCommandHandler
{
    public static async Task<IResult> HandleAsync(RefreshCommand command, AppDbContext db, JwtTokenService jwt)
    {
        var storedToken = await db.RefreshTokens
            .Include(rt => rt.Usuario)
            .FirstOrDefaultAsync(rt => rt.Token == command.RefreshToken);

        if (storedToken is null
            || storedToken.IsRevoked
            || storedToken.ExpiresAtUtc <= DateTime.UtcNow
            || storedToken.Usuario is null
            || !storedToken.Usuario.IsActive)
        {
            return Results.Unauthorized();
        }

        var user = storedToken.Usuario;

        var token = jwt.GenerateToken(user.Email, user.Role);
        var (refreshToken, expiresAtUtc) = jwt.GenerateRefreshToken();

        storedToken.IsRevoked = true;
        db.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            UsuarioId = user.Id,
            ExpiresAtUtc = expiresAtUtc,
            CreatedAtUtc = DateTime.UtcNow,
        });
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            token,
            refreshToken,
            expiresIn = 3600,
            email = user.Email,
        });
    }
}