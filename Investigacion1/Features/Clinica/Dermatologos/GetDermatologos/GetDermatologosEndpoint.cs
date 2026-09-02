using System.Security.Claims;
using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Dermatologos.GetDermatologos;

public static class GetDermatologosEndpoint
{
    public static void MapGetDermatologosEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/dermatologos", async (ClaimsPrincipal user, AppDbContext db) =>
            await GetDermatologosQueryHandler.HandleAsync(new GetDermatologosQuery(), user, db))
           .WithName("GetDermatologos")
           .AllowAnonymous();
    }
}
