using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Tratamientos.GetTratamientos;

public static class GetTratamientosQueryHandler
{
    public static async Task<IResult> HandleAsync(GetTratamientosQuery query, AppDbContext db)
    {
        var tratamientos = await db.Tratamientos
            .Select(t => new
            {
                id = t.Id,
                nombre = t.Nombre,
                descripcion = t.Descripcion,
            })
            .ToListAsync();

        return Results.Ok(tratamientos);
    }
}