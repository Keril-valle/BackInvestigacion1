using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Dermatologos.GetDermatologos;

public static class GetDermatologosQueryHandler
{
    public static async Task<IResult> HandleAsync(
        GetDermatologosQuery query,
        AppDbContext db)
    {
        var dermatologos = await db.Dermatologos
            .Select(d => new
            {
                id = d.Id,
                nombre = d.Nombre,
                especialidad = d.Especialidad,
            })
            .ToListAsync();

        return Results.Ok(dermatologos);
    }
}