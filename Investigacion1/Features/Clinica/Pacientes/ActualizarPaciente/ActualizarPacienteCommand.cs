using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Clinica.Pacientes.ActualizarPaciente;

public class ActualizarPacienteCommand
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    public string? Telefono { get; set; }

    public DateOnly? FechaNacimiento { get; set; }
}