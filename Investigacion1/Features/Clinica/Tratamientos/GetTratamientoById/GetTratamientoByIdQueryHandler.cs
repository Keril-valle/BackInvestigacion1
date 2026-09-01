using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Tratamientos.GetTratamientoById;

public static class GetTratamientoByIdQueryHandler
{
    public static async Task<IResult> HandleAsync(GetTratamientoByIdQuery query, AppDbContext db)
    {
        var tratamiento = await db.Tratamientos
            .Where(t => t.Id == query.Id)
            .Select(t => new
            {
                id = t.Id,
                nombre = t.Nombre,
                descripcion = t.Descripcion,
            })
            .FirstOrDefaultAsync();

        if (tratamiento is null)
            return Results.NotFound(new { message = "El tratamiento no existe" });

        return Results.Ok(tratamiento);
    }
}