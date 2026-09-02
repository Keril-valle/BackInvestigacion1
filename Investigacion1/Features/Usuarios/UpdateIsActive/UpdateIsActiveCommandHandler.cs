using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Usuarios.UpdateIsActive;

public static class UpdateIsActiveCommandHandler
{
    public static async Task<IResult> HandleAsync(int id, UpdateIsActiveCommand command, AppDbContext db)
    {
        var usuario = await db.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        if (usuario is null)
            return Results.NotFound(new { message = "Usuario no encontrado" });

        usuario.IsActive = command.IsActive;
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = usuario.Id,
            email = usuario.Email,
            isActive = usuario.IsActive,
        });
    }
}
