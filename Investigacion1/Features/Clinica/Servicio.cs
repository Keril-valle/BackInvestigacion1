namespace Investigacion1.Features.Clinica;

public class Servicio
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = string.Empty;
    public int DuracionMinutos { get; set; }
    public decimal Precio { get; set; }
    public bool Activo { get; set; } = true;

    public ICollection<Cita> Citas { get; set; } = new List<Cita>();
}