using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Tratamientos.GetTratamientoById;

public static class GetTratamientoByIdEndpoint
{
    public static void MapGetTratamientoByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/tratamientos/{id:guid}", async (Guid id, AppDbContext db) =>
            await GetTratamientoByIdQueryHandler.HandleAsync(new GetTratamientoByIdQuery { Id = id }, db))
           .WithName("GetTratamientoById")
           .AllowAnonymous();
    }
}