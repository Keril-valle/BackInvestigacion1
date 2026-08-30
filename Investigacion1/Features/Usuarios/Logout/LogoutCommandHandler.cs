using System.Security.Claims;
using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Usuarios.Logout;

public static class LogoutCommandHandler
{
    public static async Task<IResult> HandleAsync(
        LogoutCommand command,
        ClaimsPrincipal user,
        AppDbContext db)
    {
        var email = user.GetEmail();
        if (email is null)
        {
            return Results.Unauthorized();
        }

        var usuario = await db.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario is null)
        {
            return Results.Unauthorized();
        }

        var activos = await db.RefreshTokens
            .Where(rt => rt.UsuarioId == usuario.Id && !rt.IsRevoked)
            .ToListAsync();

        foreach (var rt in activos)
        {
            rt.IsRevoked = true;
        }
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            message = "Sesión cerrada correctamente",
        });
    }
}
