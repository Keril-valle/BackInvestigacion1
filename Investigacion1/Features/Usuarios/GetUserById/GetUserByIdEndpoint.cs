using Investigacion1.Persistence;

namespace Investigacion1.Features.Usuarios.GetUserById;

public static class GetUserByIdEndpoint
{
    public static void MapGetUserByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id:int}", async (int id, AppDbContext db) =>
            await GetUserByIdQueryHandler.HandleAsync(new GetUserByIdQuery { Id = id }, db))
           .WithName("GetUserById")
           .RequireAuthorization("Admin");
    }
}
