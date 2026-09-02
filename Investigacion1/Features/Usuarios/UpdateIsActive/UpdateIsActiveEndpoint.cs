using Investigacion1.Persistence;

namespace Investigacion1.Features.Usuarios.UpdateIsActive;

public static class UpdateIsActiveEndpoint
{
    public static void MapUpdateIsActiveEndpoint(this IEndpointRouteBuilder app)
    {
        // Ruta separada de expiración: activa/desactiva un usuario únicamente.
        app.MapPatch("/users/{id:int}/is-active", async (int id, UpdateIsActiveCommand command, AppDbContext db) =>
                await UpdateIsActiveCommandHandler.HandleAsync(id, command, db))
           .WithName("UpdateIsActive")
           .RequireAuthorization("Admin");
    }
}
