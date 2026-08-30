using Investigacion1.Persistence;

namespace Investigacion1.Features.Usuarios.GetUsers;

public static class GetUsersEndpoint
{
    public static void MapGetUsersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (AppDbContext db) =>
            await GetUsersQueryHandler.HandleAsync(new GetUsersQuery(), db))
           .WithName("GetUsers")
           .RequireAuthorization("Admin");
    }
}
