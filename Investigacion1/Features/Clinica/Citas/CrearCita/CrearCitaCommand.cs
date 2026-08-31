using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Clinica.Citas.CrearCita;

public class CrearCitaCommand
{
    [Required]
    public Guid ServicioId { get; set; }

    public Guid DermatologoId { get; set; }

    [Required]
    public DateTime FechaHora { get; set; }

    public string? Notas { get; set; }
}