using Investigacion1.Persistence;

namespace Investigacion1.Features.Usuarios.UpdateSubscriptionExpiration;

public static class UpdateSubscriptionExpirationEndpoint
{
    public static void MapUpdateSubscriptionExpirationEndpoint(this IEndpointRouteBuilder app)
    {
        // Ruta separada de is-active: actualiza únicamente SubscriptionExpirationDate.
        app.MapPatch("/users/{id:int}/subscription-expiration", async (int id, UpdateSubscriptionExpirationCommand command, AppDbContext db) =>
                await UpdateSubscriptionExpirationCommandHandler.HandleAsync(id, command, db))
           .WithName("UpdateSubscriptionExpiration")
           .RequireAuthorization("Admin");
    }
}
