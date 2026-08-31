using System.Security.Claims;
using Investigacion1.Features.Usuarios;
using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Citas.CrearCita;

public static class CrearCitaCommandHandler
{
    public static async Task<IResult> HandleAsync(
        CrearCitaCommand command,
        ClaimsPrincipal user,
        AppDbContext db)
    {
        var errors = new Dictionary<string, string[]>();

        if (command.FechaHora == default)
            errors["fechaHora"] = ["La fecha y hora son requeridas"];

        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var servicio = await db.Servicios.FirstOrDefaultAsync(s => s.Id == command.ServicioId && s.Activo);
        if (servicio is null)
            return Results.BadRequest(new { message = "El servicio seleccionado no existe o no está activo" });

        if (command.FechaHora < DateTime.UtcNow)
            return Results.BadRequest(new { message = "No se pueden agendar citas en el pasado" });

        Guid dermatologoId = command.DermatologoId;
        if (dermatologoId == Guid.Empty)
        {
            var primerDermatologo = await db.Dermatologos.FirstOrDefaultAsync();
            if (primerDermatologo is null)
                return Results.BadRequest(new { message = "No hay dermatólogos disponibles en este momento" });
            dermatologoId = primerDermatologo.Id;
        }
        else
        {
            var existe = await db.Dermatologos.AnyAsync(d => d.Id == dermatologoId);
            if (!existe)
                return Results.BadRequest(new { message = "El dermatólogo seleccionado no existe" });
        }

        var email = user.GetEmail();
        if (email is null)
            return Results.Unauthorized();

        var usuario = await db.Usuarios
            .Include(u => u.Paciente)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (usuario is null)
            return Results.Unauthorized();

        if (usuario.Paciente is null)
            return Results.BadRequest(new { message = "No se encontró el perfil del paciente" });

        var cita = new Cita
        {
            PacienteId = usuario.Paciente.Id,
            DermatologoId = dermatologoId,
            ServicioId = command.ServicioId,
            FechaHora = command.FechaHora,
            Estado = "pendiente",
            Notas = command.Notas,
        };

        db.Citas.Add(cita);
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = cita.Id,
            servicioId = cita.ServicioId,
            dermatologoId = cita.DermatologoId,
            fechaHora = cita.FechaHora,
            estado = cita.Estado,
            notas = cita.Notas,
            mensaje = "Tu cita fue solicitada y quedó pendiente de confirmación",
        });
    }
}