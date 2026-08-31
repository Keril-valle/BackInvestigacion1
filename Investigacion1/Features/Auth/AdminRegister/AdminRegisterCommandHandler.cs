using System.Security.Claims;
using System.Text.RegularExpressions;
using Investigacion1.Features.Clinica;
using Investigacion1.Features.Usuarios;
using Investigacion1.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Investigacion1.Features.Auth.AdminRegister;

public static class AdminRegisterCommandHandler
{
    public static async Task<IResult> HandleAsync(AdminRegisterCommand command, AppDbContext db, ClaimsPrincipal user)
    {
        var errors = new Dictionary<string, string[]>();

        if (string.IsNullOrWhiteSpace(command.Nombre) || command.Nombre.Length < 3)
            errors["nombre"] = ["El nombre debe tener al menos 3 caracteres"];

        if (string.IsNullOrWhiteSpace(command.Email) || !Regex.IsMatch(command.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            errors["email"] = ["El email no es válido"];

        if (string.IsNullOrWhiteSpace(command.Password)
            || command.Password.Length < 6
            || !Regex.IsMatch(command.Password, @"[A-Za-z]")
            || !Regex.IsMatch(command.Password, @"\d"))
            errors["password"] = ["La contraseña debe tener al menos 6 caracteres, una letra y un número"];

        if (string.IsNullOrWhiteSpace(command.NumeroLicencia))
            errors["numeroLicencia"] = ["El número de licencia es requerido"];

        if (errors.Count > 0)
            return Results.ValidationProblem(errors);

        var hayAdminRegistrado = await db.Usuarios.AnyAsync(u => u.Role == Role.Admin);
        if (hayAdminRegistrado)
        {
            if (user.Identity?.IsAuthenticated != true)
                return Results.Unauthorized();

            if (!user.IsInRole(Role.Admin))
                return Results.Forbid();
        }

        var existingUser = await db.Usuarios.FirstOrDefaultAsync(u => u.Email == command.Email);
        if (existingUser is not null)
        {
            return Results.BadRequest(new { message = "Usuario ya existe" });
        }

        var licenciaExistente = await db.Dermatologos.AnyAsync(d => d.NumeroLicencia == command.NumeroLicencia);
        if (licenciaExistente)
        {
            return Results.BadRequest(new { message = "El número de licencia ya está registrado" });
        }

        var usuario = new Usuario
        {
            Email = command.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(command.Password, 15),
            Role = Usuarios.Role.Admin,
            IsActive = true,
            SubscriptionExpirationDate = DateTime.UtcNow.AddYears(1),
            Dermatologo = new Dermatologo
            {
                Nombre = command.Nombre,
                NumeroLicencia = command.NumeroLicencia,
            },
        };

        db.Usuarios.Add(usuario);
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            id = usuario.Id,
            nombre = usuario.Dermatologo?.Nombre,
            email = usuario.Email,
            role = usuario.Role,
            numeroLicencia = usuario.Dermatologo?.NumeroLicencia,
        });
    }
}