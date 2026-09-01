using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.CitaTratamientos.GetCitaTratamientoById;

public static class GetCitaTratamientoByIdQueryHandler
{
    public static async Task<IResult> HandleAsync(GetCitaTratamientoByIdQuery query, AppDbContext db)
    {
        var citaTratamiento = await db.CitaTratamientos
            .Where(ct => ct.Id == query.Id)
            .Select(ct => new
            {
                id = ct.Id,
                citaId = ct.CitaId,
                tratamientoId = ct.TratamientoId,
                observaciones = ct.Observaciones,
            })
            .FirstOrDefaultAsync();

        if (citaTratamiento is null)
            return Results.NotFound(new { message = "La asignación de tratamiento no existe" });

        return Results.Ok(citaTratamiento);
    }
}