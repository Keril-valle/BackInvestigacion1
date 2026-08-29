namespace Investigacion1.Features.Auth.Register;

public static class RegisterEndpoint
{
    public static void MapRegisterEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", RegisterCommandHandler.HandleAsync)
           .WithName("Register")
           .AllowAnonymous();
    }
}