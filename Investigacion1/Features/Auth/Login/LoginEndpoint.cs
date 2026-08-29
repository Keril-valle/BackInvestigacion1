namespace Investigacion1.Features.Auth.Login;

public static class LoginEndpoint
{
    public static void MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", LoginQueryHandler.HandleAsync)
           .WithName("Login")
           .AllowAnonymous();
    }
}