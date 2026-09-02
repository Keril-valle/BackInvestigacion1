namespace Investigacion1.Features.Clinica.Servicios.CrearServicio;

public static class CrearServicioEndpoint
{
    public static void MapCrearServicioEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/servicios", CrearServicioCommandHandler.HandleAsync)
           .WithName("CrearServicio")
           .RequireAuthorization("Admin");
    }
}
