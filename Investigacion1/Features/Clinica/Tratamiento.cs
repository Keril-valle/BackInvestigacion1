namespace Investigacion1.Features.Clinica;

public class Tratamiento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nombre { get; set; } = string.Empty;
    public string? Descripcion { get; set; }

    public ICollection<CitaTratamiento> CitaTratamientos { get; set; } = new List<CitaTratamiento>();
}