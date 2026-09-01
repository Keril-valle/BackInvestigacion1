using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Pacientes.GetPacienteById;

public static class GetPacienteByIdQueryHandler
{
    public static async Task<IResult> HandleAsync(GetPacienteByIdQuery query, AppDbContext db)
    {
        var paciente = await db.Pacientes
            .Where(p => p.Id == query.Id)
            .Select(p => new
            {
                id = p.Id,
                usuarioId = p.UsuarioId,
                nombre = p.Nombre,
                telefono = p.Telefono,
                fechaNacimiento = p.FechaNacimiento,
                email = p.Usuario.Email,
            })
            .FirstOrDefaultAsync();

        if (paciente is null)
            return Results.NotFound(new { message = "El paciente no existe" });

        return Results.Ok(paciente);
    }
}