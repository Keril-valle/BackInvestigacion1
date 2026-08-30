using System.Security.Claims;
using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Usuarios.GetCurrentUser;

public static class GetCurrentUserQueryHandler
{
    public static async Task<IResult> HandleAsync(
        GetCurrentUserQuery query,
        ClaimsPrincipal user,
        AppDbContext db)
    {
        var email = user.GetEmail();
        if (email is null)
        {
            return Results.Unauthorized();
        }

        var usuario = await db.Usuarios
            .Where(u => u.Email == email)
            .Select(u => new
            {
                u.Id,
                u.Nombre,
                u.Email,
                u.Role,
                u.IsActive,
                u.SubscriptionExpirationDate,
            })
            .FirstOrDefaultAsync();

        if (usuario is null)
        {
            return Results.Unauthorized();
        }

        return Results.Ok(usuario);
    }
}
