using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Dermatologos.ActualizarDermatologo;

public static class ActualizarDermatologoEndpoint
{
    public static void MapActualizarDermatologoEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/dermatologos/{id:guid}", async (Guid id, ActualizarDermatologoCommand command, AppDbContext db) =>
            await ActualizarDermatologoCommandHandler.HandleAsync(id, command, db))
           .WithName("ActualizarDermatologo")
           .RequireAuthorization("Admin");
    }
}
