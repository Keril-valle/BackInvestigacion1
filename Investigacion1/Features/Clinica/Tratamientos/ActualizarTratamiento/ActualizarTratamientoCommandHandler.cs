using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Tratamientos.ActualizarTratamiento;

public static class ActualizarTratamientoCommandHandler
{
    public static async Task<IResult> HandleAsync(Guid id, ActualizarTratamientoCommand command, AppDbContext db)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Nombre) || command.Nombre.Length < 3)
            errors["nombre"] = ["El nombre debe tener al menos 3 caracteres"];

        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var tratamiento = await db.Tratamientos.FirstOrDefaultAsync(t => t.Id == id);
        if (tratamiento is null)
            return Results.NotFound(new { message = "El tratamiento no existe" });

        tratamiento.Nombre = command.Nombre;
        tratamiento.Descripcion = command.Descripcion;

        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = tratamiento.Id,
            nombre = tratamiento.Nombre,
            descripcion = tratamiento.Descripcion,
        });
    }
}