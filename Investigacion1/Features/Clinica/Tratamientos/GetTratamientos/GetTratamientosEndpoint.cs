using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Tratamientos.GetTratamientos;

public static class GetTratamientosEndpoint
{
    public static void MapGetTratamientosEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/tratamientos", async (AppDbContext db) =>
            await GetTratamientosQueryHandler.HandleAsync(new GetTratamientosQuery(), db))
           .WithName("GetTratamientos")
           .AllowAnonymous();
    }
}