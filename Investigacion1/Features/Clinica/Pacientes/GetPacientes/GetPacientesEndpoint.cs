using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Pacientes.GetPacientes;

public static class GetPacientesEndpoint
{
    public static void MapGetPacientesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/pacientes", async (AppDbContext db) =>
            await GetPacientesQueryHandler.HandleAsync(new GetPacientesQuery(), db))
           .WithName("GetPacientes")
           .RequireAuthorization("Admin");
    }
}