using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Auth.Login;

public static class LoginQueryHandler
{
    public static async Task<IResult> HandleAsync(LoginQuery query, AppDbContext db, JwtTokenService jwt)
    {
        var user = await db.Usuarios.FirstOrDefaultAsync(u => u.Email == query.Email);
        if (user is null || !BCrypt.Net.BCrypt.Verify(query.Password, user.Password))
        {
            return Results.Unauthorized();
        }

        var token = jwt.GenerateToken(user.Email, user.Role);

        return Results.Ok(new
        {
            token,
            email = user.Email,
        });
    }
}