using System.Security.Claims;
using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Citas.CrearCita;

public static class CrearCitaEndpoint
{
    public static void MapCrearCitaEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/citas", async (CrearCitaCommand command, ClaimsPrincipal user, AppDbContext db) =>
            await CrearCitaCommandHandler.HandleAsync(command, user, db))
           .WithName("CrearCita")
           .RequireAuthorization();
    }
}