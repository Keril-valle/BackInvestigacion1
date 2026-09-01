using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.CitaTratamientos.EliminarCitaTratamiento;

public static class EliminarCitaTratamientoEndpoint
{
    public static void MapEliminarCitaTratamientoEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/cita-tratamientos/{id:guid}", async (Guid id, AppDbContext db) =>
            await EliminarCitaTratamientoCommandHandler.HandleAsync(id, db))
           .WithName("EliminarCitaTratamiento")
           .RequireAuthorization("Admin");
    }
}