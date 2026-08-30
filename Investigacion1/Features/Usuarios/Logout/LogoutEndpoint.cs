using System.Security.Claims;
using Investigacion1.Persistence;

namespace Investigacion1.Features.Usuarios.Logout;

public static class LogoutEndpoint
{
    public static void MapLogoutEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/logout", async (ClaimsPrincipal user, AppDbContext db) =>
            await LogoutCommandHandler.HandleAsync(new LogoutCommand(), user, db))
           .WithName("Logout")
           .RequireAuthorization();
    }
}
