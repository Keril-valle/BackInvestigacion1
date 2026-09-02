using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Dermatologos.ActualizarDermatologo;

public static class ActualizarDermatologoCommandHandler
{
    public static async Task<IResult> HandleAsync(Guid id, ActualizarDermatologoCommand command, AppDbContext db)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Nombre) || command.Nombre.Length < 3)
            errors["nombre"] = ["El nombre debe tener al menos 3 caracteres"];

        if (string.IsNullOrWhiteSpace(command.NumeroLicencia))
            errors["numeroLicencia"] = ["El número de licencia es requerido"];

        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var dermatologo = await db.Dermatologos.FirstOrDefaultAsync(d => d.Id == id);
        if (dermatologo is null)
            return Results.NotFound(new { message = "El dermatólogo no existe" });

        var licenciaEnUso = await db.Dermatologos
            .AnyAsync(d => d.Id != id && d.NumeroLicencia == command.NumeroLicencia);
        if (licenciaEnUso)
            return Results.BadRequest(new { message = "El número de licencia ya está registrado" });

        dermatologo.Nombre = command.Nombre;
        dermatologo.Especialidad = command.Especialidad;
        dermatologo.NumeroLicencia = command.NumeroLicencia;

        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = dermatologo.Id,
            nombre = dermatologo.Nombre,
            especialidad = dermatologo.Especialidad,
            numeroLicencia = dermatologo.NumeroLicencia,
        });
    }
}
