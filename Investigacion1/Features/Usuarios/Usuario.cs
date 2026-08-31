using Investigacion1.Features.Clinica;

namespace Investigacion1.Features.Usuarios;

public class Usuario
{
    public int Id { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = Usuarios.Role.Subscription_L1;

    public bool IsActive { get; set; } = true;

    public DateTime SubscriptionExpirationDate { get; set; } = DateTime.UtcNow.AddYears(1);

    public Dermatologo? Dermatologo { get; set; }

    public Paciente? Paciente { get; set; }
}