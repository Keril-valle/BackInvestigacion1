using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Pacientes.ActualizarPaciente;

public static class ActualizarPacienteCommandHandler
{
    public static async Task<IResult> HandleAsync(Guid id, ActualizarPacienteCommand command, AppDbContext db)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Nombre) || command.Nombre.Length < 3)
            errors["nombre"] = ["El nombre debe tener al menos 3 caracteres"];

        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var paciente = await db.Pacientes.FirstOrDefaultAsync(p => p.Id == id);
        if (paciente is null)
            return Results.NotFound(new { message = "El paciente no existe" });

        paciente.Nombre = command.Nombre;
        paciente.Telefono = command.Telefono;
        paciente.FechaNacimiento = command.FechaNacimiento;

        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = paciente.Id,
            nombre = paciente.Nombre,
            telefono = paciente.Telefono,
            fechaNacimiento = paciente.FechaNacimiento,
        });
    }
}