using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Pacientes.GetPacienteById;

public static class GetPacienteByIdEndpoint
{
    public static void MapGetPacienteByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/pacientes/{id:guid}", async (Guid id, AppDbContext db) =>
            await GetPacienteByIdQueryHandler.HandleAsync(new GetPacienteByIdQuery { Id = id }, db))
           .WithName("GetPacienteById")
           .RequireAuthorization("Admin");
    }
}