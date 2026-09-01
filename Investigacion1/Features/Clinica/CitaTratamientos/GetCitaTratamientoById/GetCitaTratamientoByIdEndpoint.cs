using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.CitaTratamientos.GetCitaTratamientoById;

public static class GetCitaTratamientoByIdEndpoint
{
    public static void MapGetCitaTratamientoByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/cita-tratamientos/{id:guid}", async (Guid id, AppDbContext db) =>
            await GetCitaTratamientoByIdQueryHandler.HandleAsync(new GetCitaTratamientoByIdQuery { Id = id }, db))
           .WithName("GetCitaTratamientoById")
           .RequireAuthorization("Admin");
    }
}