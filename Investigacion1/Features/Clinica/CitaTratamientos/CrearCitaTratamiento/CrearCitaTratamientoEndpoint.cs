using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.CitaTratamientos.CrearCitaTratamiento;

public static class CrearCitaTratamientoEndpoint
{
    public static void MapCrearCitaTratamientoEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/cita-tratamientos", async (CrearCitaTratamientoCommand command, AppDbContext db) =>
            await CrearCitaTratamientoCommandHandler.HandleAsync(command, db))
           .WithName("CrearCitaTratamiento")
           .RequireAuthorization("Admin");
    }
}