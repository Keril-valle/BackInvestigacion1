using System.Text.RegularExpressions;
using Investigacion1.Features.Clinica;
using Investigacion1.Features.Usuarios;
using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Auth.Register;

public static class RegisterCommandHandler
{
    public static async Task<IResult> HandleAsync(RegisterCommand command, AppDbContext db)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Nombre) || command.Nombre.Length < 3)
            errors["nombre"] = ["El nombre debe tener al menos 3 caracteres"];

        if (string.IsNullOrWhiteSpace(command.Email) || !Regex.IsMatch(command.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            errors["email"] = ["El email no es válido"];

        if (string.IsNullOrWhiteSpace(command.Password) || command.Password.Length < 6)
            errors["password"] = ["La contraseña debe tener al menos 6 caracteres"];

        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var existingUser = await db.Usuarios.FirstOrDefaultAsync(u => u.Email == command.Email);
        if (existingUser is not null)
        {
            return Results.BadRequest(new { message = "Usuario ya existe" });
        }

        var usuario = new Usuario
        {
            Email = command.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(command.Password, 15),
            Role = Role.Subscription_L1,
            IsActive = true,
            SubscriptionExpirationDate = DateTime.UtcNow.AddYears(1),
            Paciente = new Paciente
            {
                Nombre = command.Nombre,
            },
        };

        db.Usuarios.Add(usuario);
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = usuario.Id,
            nombre = usuario.Paciente?.Nombre,
            email = usuario.Email,
            role = usuario.Role,
        });
    }
}