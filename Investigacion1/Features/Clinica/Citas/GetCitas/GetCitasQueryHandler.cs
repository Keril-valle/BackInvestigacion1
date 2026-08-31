using System.Security.Claims;
using Investigacion1.Features.Usuarios;
using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Citas.GetCitas;

public static class GetCitasQueryHandler
{
    public static async Task<IResult> HandleAsync(
        GetCitasQuery query,
        ClaimsPrincipal user,
        AppDbContext db)
    {
        var email = user.GetEmail();
        if (email is null)
            return Results.Unauthorized();

        var usuario = await db.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario is null)
            return Results.Unauthorized();

        var isAdmin = user.IsInRole(Role.Admin);

        IQueryable<Cita> queryable = db.Citas
            .Include(c => c.Paciente)
            .Include(c => c.Dermatologo)
            .Include(c => c.Servicio)
            .Include(c => c.CitaTratamientos).ThenInclude(ct => ct.Tratamiento);

        if (!isAdmin)
        {
            var paciente = await db.Pacientes.FirstOrDefaultAsync(p => p.UsuarioId == usuario.Id);
            if (paciente is null)
                return Results.Ok(new object[0]);

            queryable = queryable.Where(c => c.PacienteId == paciente.Id);
        }

        var citas = await queryable
            .OrderBy(c => c.FechaHora)
            .Select(c => new
            {
                id = c.Id,
                fechaHora = c.FechaHora,
                estado = c.Estado,
                notas = c.Notas,
                servicio = new
                {
                    id = c.Servicio.Id,
                    nombre = c.Servicio.Nombre,
                },
                dermatologo = new
                {
                    id = c.Dermatologo.Id,
                    nombre = c.Dermatologo.Nombre,
                    especialidad = c.Dermatologo.Especialidad,
                },
                paciente = new
                {
                    id = c.Paciente.Id,
                    nombre = c.Paciente.Nombre,
                    telefono = c.Paciente.Telefono,
                },
                tratamientos = c.CitaTratamientos.Select(ct => new
                {
                    id = ct.Tratamiento.Id,
                    nombre = ct.Tratamiento.Nombre,
                    observaciones = ct.Observaciones,
                }),
            })
            .ToListAsync();

        return Results.Ok(citas);
    }
}
