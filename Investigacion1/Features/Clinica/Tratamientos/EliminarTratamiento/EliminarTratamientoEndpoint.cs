using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Tratamientos.EliminarTratamiento;

public static class EliminarTratamientoEndpoint
{
    public static void MapEliminarTratamientoEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/tratamientos/{id:guid}", async (Guid id, AppDbContext db) =>
            await EliminarTratamientoCommandHandler.HandleAsync(id, db))
           .WithName("EliminarTratamiento")
           .RequireAuthorization("Admin");
    }
}