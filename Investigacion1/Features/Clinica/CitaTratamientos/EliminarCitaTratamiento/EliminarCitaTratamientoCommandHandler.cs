using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.CitaTratamientos.EliminarCitaTratamiento;

public static class EliminarCitaTratamientoCommandHandler
{
    public static async Task<IResult> HandleAsync(Guid id, AppDbContext db)
    {
        var citaTratamiento = await db.CitaTratamientos.FirstOrDefaultAsync(ct => ct.Id == id);
        if (citaTratamiento is null)
            return Results.NotFound(new { message = "La asignación de tratamiento no existe" });

        db.CitaTratamientos.Remove(citaTratamiento);
        await db.SaveChangesAsync();

        return Results.Ok(new { message = "Asignación de tratamiento eliminada" });
    }
}