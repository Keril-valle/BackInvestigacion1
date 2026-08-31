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
                Nombre = u.Role == Investigacion1.Features.Usuarios.Role.Admin
                    ? (string?)u.Dermatologo!.Nombre
                    : (string?)u.Paciente!.Nombre,
                u.Email,
                u.Role,
                u.IsActive,
                u.SubscriptionExpirationDate,
            })
            .ToListAsync();

        return Results.Ok(usuarios);
    }
}