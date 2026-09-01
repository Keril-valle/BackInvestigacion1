using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Pacientes.CrearPaciente;

public static class CrearPacienteEndpoint
{
    public static void MapCrearPacienteEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/pacientes", async (CrearPacienteCommand command, AppDbContext db) =>
            await CrearPacienteCommandHandler.HandleAsync(command, db))
           .WithName("CrearPaciente")
           .RequireAuthorization("Admin");
    }
}