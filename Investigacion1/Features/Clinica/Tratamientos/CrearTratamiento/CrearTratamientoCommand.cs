using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Clinica.Tratamientos.CrearTratamiento;

public class CrearTratamientoCommand
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }
}