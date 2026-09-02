using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Usuarios.UpdateSubscriptionExpiration;

public static class UpdateSubscriptionExpirationCommandHandler
{
    public static async Task<IResult> HandleAsync(int id, UpdateSubscriptionExpirationCommand command, AppDbContext db)
    {
        if (command.SubscriptionExpirationDate == default)
        {
            return Results.ValidationProblem(new Dictionary<string, string[]>
            {
                ["subscriptionExpirationDate"] = ["La fecha de expiración es requerida"],
            });
        }

        var usuario = await db.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario is null)
            return Results.NotFound(new { message = "Usuario no encontrado" });

        usuario.SubscriptionExpirationDate = command.SubscriptionExpirationDate;
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = usuario.Id,
            email = usuario.Email,
            subscriptionExpirationDate = usuario.SubscriptionExpirationDate,
        });
    }
}
