using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.CitaTratamientos.ActualizarCitaTratamiento;

public static class ActualizarCitaTratamientoEndpoint
{
    public static void MapActualizarCitaTratamientoEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/cita-tratamientos/{id:guid}", async (Guid id, ActualizarCitaTratamientoCommand command, AppDbContext db) =>
            await ActualizarCitaTratamientoCommandHandler.HandleAsync(id, command, db))
           .WithName("ActualizarCitaTratamiento")
           .RequireAuthorization("Admin");
    }
}