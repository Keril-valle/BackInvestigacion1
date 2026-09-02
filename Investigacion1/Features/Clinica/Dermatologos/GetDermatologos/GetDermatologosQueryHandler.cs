using System.Security.Claims;
using Investigacion1.Features.Usuarios;
using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Clinica.Dermatologos.GetDermatologos;

public static class GetDermatologosQueryHandler
{
    public static async Task<IResult> HandleAsync(
        GetDermatologosQuery query,
        ClaimsPrincipal user,
        AppDbContext db)
    {
        var esAdmin = user.Identity?.IsAuthenticated == true && user.IsInRole(Role.Admin);

        // Un Admin ve todos los dermatólogos (incluye inactivos) con datos de gestión;
        // el público solo ve lo básico de los dermatólogos activos.
        if (esAdmin)
        {
            var dermatologosAdmin = await db.Dermatologos
                .Select(d => new
                {
                    id = d.Id,
                    usuarioId = d.UsuarioId,
                    nombre = d.Nombre,
                    especialidad = d.Especialidad,
                    numeroLicencia = d.NumeroLicencia,
                    email = d.Usuario.Email,
                    isActive = d.Usuario.IsActive,
                })
                .ToListAsync();

            return Results.Ok(dermatologosAdmin);
        }

        var dermatologos = await db.Dermatologos
            .Where(d => d.Usuario.IsActive)
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
