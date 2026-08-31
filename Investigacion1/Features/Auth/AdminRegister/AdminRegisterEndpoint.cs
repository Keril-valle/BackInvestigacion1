namespace Investigacion1.Features.Auth.AdminRegister;

public static class AdminRegisterEndpoint
{
    public static void MapAdminRegisterEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/admin/register", AdminRegisterCommandHandler.HandleAsync)
           .WithName("AdminRegister");
    }
}