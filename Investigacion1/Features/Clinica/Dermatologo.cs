using Investigacion1.Features.Usuarios;

namespace Investigacion1.Features.Clinica;

public class Dermatologo
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int UsuarioId { get; set; }

    public Usuario Usuario { get; set; } = null!;

    public string Nombre { get; set; } = string.Empty;

    public string? Especialidad { get; set; }

    public string NumeroLicencia { get; set; } = string.Empty;

    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}