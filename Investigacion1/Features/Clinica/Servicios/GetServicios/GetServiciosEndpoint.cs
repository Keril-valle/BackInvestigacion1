using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Servicios.GetServicios;

public static class GetServiciosEndpoint
{
    public static void MapGetServiciosEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/servicios", async (AppDbContext db) =>
            await GetServiciosQueryHandler.HandleAsync(new GetServiciosQuery(), db))
           .WithName("GetServicios")
           .AllowAnonymous();
    }
}