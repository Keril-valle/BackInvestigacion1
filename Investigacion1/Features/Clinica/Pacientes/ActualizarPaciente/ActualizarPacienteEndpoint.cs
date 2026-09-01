using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Pacientes.ActualizarPaciente;

public static class ActualizarPacienteEndpoint
{
    public static void MapActualizarPacienteEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/pacientes/{id:guid}", async (Guid id, ActualizarPacienteCommand command, AppDbContext db) =>
            await ActualizarPacienteCommandHandler.HandleAsync(id, command, db))
           .WithName("ActualizarPaciente")
           .RequireAuthorization("Admin");
    }
}