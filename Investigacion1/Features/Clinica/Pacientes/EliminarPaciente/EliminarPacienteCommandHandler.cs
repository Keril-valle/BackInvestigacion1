using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Pacientes.EliminarPaciente;

public static class EliminarPacienteCommandHandler
{
    public static async Task<IResult> HandleAsync(Guid id, AppDbContext db)
    {
        var paciente = await db.Pacientes.FirstOrDefaultAsync(p => p.Id == id);
        if (paciente is null)
            return Results.NotFound(new { message = "El paciente no existe" });

        db.Pacientes.Remove(paciente);
        await db.SaveChangesAsync();

        return Results.Ok(new { message = "Paciente eliminado" });
    }
}