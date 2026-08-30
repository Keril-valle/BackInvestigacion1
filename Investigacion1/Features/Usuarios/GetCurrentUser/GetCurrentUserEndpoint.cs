using System.Security.Claims;
using Investigacion1.Persistence;

namespace Investigacion1.Features.Usuarios.GetCurrentUser;

public static class GetCurrentUserEndpoint
{
    public static void MapGetCurrentUserEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/me", async (ClaimsPrincipal user, AppDbContext db) =>
            await GetCurrentUserQueryHandler.HandleAsync(new GetCurrentUserQuery(), user, db))
           .WithName("GetCurrentUser")
           .RequireAuthorization();
    }
}
