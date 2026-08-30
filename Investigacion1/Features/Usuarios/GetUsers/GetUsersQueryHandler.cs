using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Usuarios.GetUsers;

public static class GetUsersQueryHandler
{
    public static async Task<IResult> HandleAsync(
        GetUsersQuery query,
        AppDbContext db)
    {
        var usuarios = await db.Usuarios
            .Select(u => new
            {
                u.Id,
                u.Nombre,
                u.Email,
                u.Role,
                u.IsActive,
                u.SubscriptionExpirationDate,
            })
            .ToListAsync();

        return Results.Ok(usuarios);
    }
}
