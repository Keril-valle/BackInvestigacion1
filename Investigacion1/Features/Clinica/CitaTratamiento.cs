namespace Investigacion1.Features.Clinica;

public class CitaTratamiento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CitaId { get; set; }
    public Guid TratamientoId { get; set; }
    public string? Observaciones { get; set; }

    public Cita Cita { get; set; } = null!;
    public Tratamiento Tratamiento { get; set; } = null!;
}