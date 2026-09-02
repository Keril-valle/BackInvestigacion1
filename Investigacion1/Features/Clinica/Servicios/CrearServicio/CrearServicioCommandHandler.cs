using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Servicios.CrearServicio;

public static class CrearServicioCommandHandler
{
    public static async Task<IResult> HandleAsync(CrearServicioCommand command, AppDbContext db)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Nombre) || command.Nombre.Length < 3)
            errors["nombre"] = ["El nombre debe tener al menos 3 caracteres"];

        if (command.DuracionMinutos <= 0)
            errors["duracionMinutos"] = ["La duración debe ser mayor a 0"];

        if (command.Precio < 0)
            errors["precio"] = ["El precio no puede ser negativo"];

        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var servicio = new Servicio
        {
            Nombre = command.Nombre,
            DuracionMinutos = command.DuracionMinutos,
            Precio = command.Precio,
        };

        db.Servicios.Add(servicio);
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = servicio.Id,
            nombre = servicio.Nombre,
            duracionMinutos = servicio.DuracionMinutos,
            precio = servicio.Precio,
            activo = servicio.Activo,
        });
    }
}
