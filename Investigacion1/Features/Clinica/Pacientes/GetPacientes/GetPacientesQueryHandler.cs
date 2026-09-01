using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Pacientes.GetPacientes;

public static class GetPacientesQueryHandler
{
    public static async Task<IResult> HandleAsync(GetPacientesQuery query, AppDbContext db)
    {
        var pacientes = await db.Pacientes
            .Select(p => new
            {
                id = p.Id,
                usuarioId = p.UsuarioId,
                nombre = p.Nombre,
                telefono = p.Telefono,
                fechaNacimiento = p.FechaNacimiento,
                email = p.Usuario.Email,
            })
            .ToListAsync();

        return Results.Ok(pacientes);
    }
}