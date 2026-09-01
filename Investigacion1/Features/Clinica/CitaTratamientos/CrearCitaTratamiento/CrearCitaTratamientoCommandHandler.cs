using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.CitaTratamientos.CrearCitaTratamiento;

public static class CrearCitaTratamientoCommandHandler
{
    public static async Task<IResult> HandleAsync(CrearCitaTratamientoCommand command, AppDbContext db)
    {
        var citaExiste = await db.Citas.AnyAsync(c => c.Id == command.CitaId);
        if (!citaExiste)
            return Results.BadRequest(new { message = "La cita seleccionada no existe" });

        var tratamientoExiste = await db.Tratamientos.AnyAsync(t => t.Id == command.TratamientoId);
        if (!tratamientoExiste)
            return Results.BadRequest(new { message = "El tratamiento seleccionado no existe" });

        var yaAsignado = await db.CitaTratamientos.AnyAsync(ct =>
            ct.CitaId == command.CitaId && ct.TratamientoId == command.TratamientoId);
        if (yaAsignado)
            return Results.BadRequest(new { message = "El tratamiento ya está asignado a esta cita" });

        var citaTratamiento = new CitaTratamiento
        {
            CitaId = command.CitaId,
            TratamientoId = command.TratamientoId,
            Observaciones = command.Observaciones,
        };

        db.CitaTratamientos.Add(citaTratamiento);
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = citaTratamiento.Id,
            citaId = citaTratamiento.CitaId,
            tratamientoId = citaTratamiento.TratamientoId,
            observaciones = citaTratamiento.Observaciones,
        });
    }
}