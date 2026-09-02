using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Clinica.Servicios.ActualizarServicio;

public class ActualizarServicioCommand
{
    [Required]
    public string Nombre { get; set; } = string.Empty;

    public int DuracionMinutos { get; set; }

    public decimal Precio { get; set; }

    public bool Activo { get; set; } = true;
}
