using System.Security.Claims;
using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Pacientes.GetHistorialPaciente;

public static class GetHistorialPacienteEndpoint
{
    public static void MapGetHistorialPacienteEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/pacientes/{pacienteId:guid}/historial", async (Guid pacienteId, ClaimsPrincipal user, AppDbContext db) =>
                await GetHistorialPacienteQueryHandler.HandleAsync(new GetHistorialPacienteQuery { PacienteId = pacienteId }, user, db))
           .WithName("GetHistorialPaciente")
           .RequireAuthorization();
    }
}
