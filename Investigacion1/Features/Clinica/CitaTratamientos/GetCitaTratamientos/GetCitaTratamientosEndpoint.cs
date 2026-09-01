using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.CitaTratamientos.GetCitaTratamientos;

public static class GetCitaTratamientosEndpoint
{
    public static void MapGetCitaTratamientosEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/cita-tratamientos", async (AppDbContext db) =>
            await GetCitaTratamientosQueryHandler.HandleAsync(new GetCitaTratamientosQuery(), db))
           .WithName("GetCitaTratamientos")
           .RequireAuthorization("Admin");
    }
}