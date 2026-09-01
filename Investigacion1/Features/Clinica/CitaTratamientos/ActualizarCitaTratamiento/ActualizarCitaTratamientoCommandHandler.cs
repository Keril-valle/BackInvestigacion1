using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.CitaTratamientos.ActualizarCitaTratamiento;

public static class ActualizarCitaTratamientoCommandHandler
{
    public static async Task<IResult> HandleAsync(Guid id, ActualizarCitaTratamientoCommand command, AppDbContext db)
    {
        var citaTratamiento = await db.CitaTratamientos.FirstOrDefaultAsync(ct => ct.Id == id);
        if (citaTratamiento is null)
            return Results.NotFound(new { message = "La asignación de tratamiento no existe" });

        var tratamientoExiste = await db.Tratamientos.AnyAsync(t => t.Id == command.TratamientoId);
        if (!tratamientoExiste)
            return Results.BadRequest(new { message = "El tratamiento seleccionado no existe" });

        var yaAsignado = await db.CitaTratamientos.AnyAsync(ct =>
            ct.CitaId == citaTratamiento.CitaId
            && ct.TratamientoId == command.TratamientoId
            && ct.Id != id);
        if (yaAsignado)
            return Results.BadRequest(new { message = "El tratamiento ya está asignado a esta cita" });

        citaTratamiento.TratamientoId = command.TratamientoId;
        citaTratamiento.Observaciones = command.Observaciones;

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