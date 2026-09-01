using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Clinica.Pacientes.CrearPaciente;

public class CrearPacienteCommand
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string? Telefono { get; set; }

    public DateOnly? FechaNacimiento { get; set; }
}