using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Clinica.CitaTratamientos.CrearCitaTratamiento;

public class CrearCitaTratamientoCommand
{
    [Required]
    public Guid CitaId { get; set; }

    [Required]
    public Guid TratamientoId { get; set; }

    public string? Observaciones { get; set; }
}