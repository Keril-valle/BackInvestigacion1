using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Dermatologos.GetDermatologos;

public static class GetDermatologosEndpoint
{
    public static void MapGetDermatologosEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/dermatologos", async (AppDbContext db) =>
            await GetDermatologosQueryHandler.HandleAsync(new GetDermatologosQuery(), db))
           .WithName("GetDermatologos")
           .AllowAnonymous();
    }
}