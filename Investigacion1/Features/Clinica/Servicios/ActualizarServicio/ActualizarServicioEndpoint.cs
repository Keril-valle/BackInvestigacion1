using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Servicios.ActualizarServicio;

public static class ActualizarServicioEndpoint
{
    public static void MapActualizarServicioEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/servicios/{id:guid}", async (Guid id, ActualizarServicioCommand command, AppDbContext db) =>
            await ActualizarServicioCommandHandler.HandleAsync(id, command, db))
           .WithName("ActualizarServicio")
           .RequireAuthorization("Admin");
    }
}
