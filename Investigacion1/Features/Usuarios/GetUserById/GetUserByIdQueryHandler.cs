using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Usuarios.GetUserById;

public static class GetUserByIdQueryHandler
{
    public static async Task<IResult> HandleAsync(
        GetUserByIdQuery query,
        AppDbContext db)
    {
        var usuario = await db.Usuarios
            .Where(u => u.Id == query.Id)
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
            return Results.NotFound(new { message = "Usuario no encontrado" });
        }

        return Results.Ok(usuario);
    }
}
