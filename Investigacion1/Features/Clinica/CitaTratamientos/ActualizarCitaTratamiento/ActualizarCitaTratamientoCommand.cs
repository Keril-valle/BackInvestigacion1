using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Clinica.CitaTratamientos.ActualizarCitaTratamiento;

public class ActualizarCitaTratamientoCommand
{
    [Required]
    public Guid TratamientoId { get; set; }

    public string? Observaciones { get; set; }
}