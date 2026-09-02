using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Clinica.Dermatologos.ActualizarDermatologo;

public class ActualizarDermatologoCommand
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    public string? Especialidad { get; set; }

    [Required]
    public string NumeroLicencia { get; set; } = string.Empty;
}
