using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.CitaTratamientos.GetCitaTratamientos;

public static class GetCitaTratamientosQueryHandler
{
    public static async Task<IResult> HandleAsync(GetCitaTratamientosQuery query, AppDbContext db)
    {
        var citaTratamientos = await db.CitaTratamientos
            .Select(ct => new
            {
                id = ct.Id,
                citaId = ct.CitaId,
                tratamientoId = ct.TratamientoId,
                observaciones = ct.Observaciones,
            })
            .ToListAsync();

        return Results.Ok(citaTratamientos);
    }
}