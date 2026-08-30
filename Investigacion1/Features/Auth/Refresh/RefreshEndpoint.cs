namespace Investigacion1.Features.Auth.Refresh;

public static class RefreshEndpoint
{
    public static void MapRefreshEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/refresh", RefreshCommandHandler.HandleAsync)
           .WithName("Refresh")
           .AllowAnonymous();
    }
}