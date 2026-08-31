namespace Investigacion1.Features.Clinica;

public class Cita
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PacienteId { get; set; }
    public Guid DermatologoId { get; set; }
    public Guid ServicioId { get; set; }
    public DateTime FechaHora { get; set; }
    public string Estado { get; set; } = "pendiente";
    public string? Notas { get; set; }

    public Paciente Paciente { get; set; } = null!;
    public Dermatologo Dermatologo { get; set; } = null!;
    public Servicio Servicio { get; set; } = null!;
    public ICollection<CitaTratamiento> CitaTratamientos { get; set; } = new List<CitaTratamiento>();
}