using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Tratamientos.EliminarTratamiento;

public static class EliminarTratamientoCommandHandler
{
    public static async Task<IResult> HandleAsync(Guid id, AppDbContext db)
    {
        var tratamiento = await db.Tratamientos.FirstOrDefaultAsync(t => t.Id == id);
        if (tratamiento is null)
            return Results.NotFound(new { message = "El tratamiento no existe" });

        db.Tratamientos.Remove(tratamiento);
        await db.SaveChangesAsync();

        return Results.Ok(new { message = "Tratamiento eliminado" });
    }
}