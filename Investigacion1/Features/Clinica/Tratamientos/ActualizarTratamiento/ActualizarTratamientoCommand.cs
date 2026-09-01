using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Clinica.Tratamientos.ActualizarTratamiento;

public class ActualizarTratamientoCommand
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }
}