using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Tratamientos.CrearTratamiento;

public static class CrearTratamientoEndpoint
{
    public static void MapCrearTratamientoEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/tratamientos", async (CrearTratamientoCommand command, AppDbContext db) =>
            await CrearTratamientoCommandHandler.HandleAsync(command, db))
           .WithName("CrearTratamiento")
           .RequireAuthorization("Admin");
    }
}