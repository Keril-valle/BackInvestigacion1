using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Servicios.GetServicios;

public static class GetServiciosQueryHandler
{
    public static async Task<IResult> HandleAsync(
        GetServiciosQuery query,
        AppDbContext db)
    {
        var servicios = await db.Servicios
            .Where(s => s.Activo)
            .Select(s => new
            {
                id = s.Id,
                nombre = s.Nombre,
                duracionMinutos = s.DuracionMinutos,
                precio = s.Precio,
                activo = s.Activo,
            })
            .ToListAsync();

        return Results.Ok(servicios);
    }
}