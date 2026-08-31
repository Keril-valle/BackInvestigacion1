using System.Security.Claims;
using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Citas.GetCitas;

public static class GetCitasEndpoint
{
    public static void MapGetCitasEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/citas", async (ClaimsPrincipal user, AppDbContext db) =>
            await GetCitasQueryHandler.HandleAsync(new GetCitasQuery(), user, db))
           .WithName("GetCitas")
           .RequireAuthorization();
    }
}
