using System.Security.Claims;
using Investigacion1.Features.Usuarios;
using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Pacientes.GetHistorialPaciente;

public static class GetHistorialPacienteQueryHandler
{
    public static async Task<IResult> HandleAsync(GetHistorialPacienteQuery query, ClaimsPrincipal user, AppDbContext db)
    {
        var email = user.GetEmail();
        if (email is null)
            return Results.Unauthorized();

        // Un paciente (Subscription_L1) solo puede ver su propio historial;
        // un dermatólogo (Admin) puede ver el de cualquier paciente.
        if (!user.IsInRole(Role.Admin))
        {
            var propioPacienteId = await db.Pacientes
                .Where(p => p.Usuario.Email == email)
                .Select(p => (Guid?)p.Id)
                .FirstOrDefaultAsync();

            if (propioPacienteId is null || propioPacienteId != query.PacienteId)
                return Results.Forbid();
        }

        var paciente = await db.Pacientes
            .Where(p => p.Id == query.PacienteId)
            .Select(p => new { p.Id, p.Nombre })
            .FirstOrDefaultAsync();

        if (paciente is null)
            return Results.NotFound(new { message = "Paciente no encontrado" });

        var citas = await db.Citas
            .Where(c => c.PacienteId == query.PacienteId)
            .OrderByDescending(c => c.FechaHora)
            .Select(c => new
            {
                c.Id,
                c.FechaHora,
                c.Estado,
                c.Notas,
                Servicio = c.Servicio.Nombre,
                Dermatologo = c.Dermatologo.Nombre,
                Tratamientos = c.CitaTratamientos.Select(ct => new
                {
                    Nombre = ct.Tratamiento.Nombre,
                    ct.Observaciones,
                }),
            })
            .ToListAsync();

        return Results.Ok(new
        {
            pacienteId = paciente.Id,
            pacienteNombre = paciente.Nombre,
            citas,
        });
    }
}
