using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Clinica.Servicios.CrearServicio;

public class CrearServicioCommand
{
    [Required]
    [MinLength(3)]
    public string Nombre { get; set; } = string.Empty;

    public int DuracionMinutos { get; set; }

    public decimal Precio { get; set; }
}
