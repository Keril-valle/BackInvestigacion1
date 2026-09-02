using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Servicios.ActualizarServicio;

public static class ActualizarServicioCommandHandler
{
    public static async Task<IResult> HandleAsync(Guid id, ActualizarServicioCommand command, AppDbContext db)
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

        var servicio = await db.Servicios.FirstOrDefaultAsync(s => s.Id == id);
        if (servicio is null)
            return Results.NotFound(new { message = "El servicio no existe" });

        servicio.Nombre = command.Nombre;
        servicio.DuracionMinutos = command.DuracionMinutos;
        servicio.Precio = command.Precio;
        servicio.Activo = command.Activo;

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
