using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Tratamientos.ActualizarTratamiento;

public static class ActualizarTratamientoEndpoint
{
    public static void MapActualizarTratamientoEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/tratamientos/{id:guid}", async (Guid id, ActualizarTratamientoCommand command, AppDbContext db) =>
            await ActualizarTratamientoCommandHandler.HandleAsync(id, command, db))
           .WithName("ActualizarTratamiento")
           .RequireAuthorization("Admin");
    }
}