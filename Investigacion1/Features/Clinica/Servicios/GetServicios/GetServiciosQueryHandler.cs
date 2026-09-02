using System.Security.Claims;
using Investigacion1.Features.Usuarios;
using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Servicios.GetServicios;

public static class GetServiciosQueryHandler
{
    public static async Task<IResult> HandleAsync(
        GetServiciosQuery query,
        ClaimsPrincipal user,
        AppDbContext db)
    {
        var esAdmin = user.Identity?.IsAuthenticated == true && user.IsInRole(Role.Admin);

        // Un Admin ve todos los servicios (incluye inactivos) para poder gestionarlos;
        // el público solo ve los activos.
        var servicios = await db.Servicios
            .Where(s => esAdmin || s.Activo)
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
