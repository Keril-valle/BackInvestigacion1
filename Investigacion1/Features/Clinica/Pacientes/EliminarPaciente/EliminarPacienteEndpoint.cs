using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Pacientes.EliminarPaciente;

public static class EliminarPacienteEndpoint
{
    public static void MapEliminarPacienteEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapDelete("/pacientes/{id:guid}", async (Guid id, AppDbContext db) =>
            await EliminarPacienteCommandHandler.HandleAsync(id, db))
           .WithName("EliminarPaciente")
           .RequireAuthorization("Admin");
    }
}