using Investigacion1.Persistence;

namespace Investigacion1.Features.Clinica.Tratamientos.CrearTratamiento;

public static class CrearTratamientoCommandHandler
{
    public static async Task<IResult> HandleAsync(CrearTratamientoCommand command, AppDbContext db)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Nombre) || command.Nombre.Length < 3)
            errors["nombre"] = ["El nombre debe tener al menos 3 caracteres"];

        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var tratamiento = new Tratamiento
        {
            Nombre = command.Nombre,
            Descripcion = command.Descripcion,
        };

        db.Tratamientos.Add(tratamiento);
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = tratamiento.Id,
            nombre = tratamiento.Nombre,
            descripcion = tratamiento.Descripcion,
        });
    }
}