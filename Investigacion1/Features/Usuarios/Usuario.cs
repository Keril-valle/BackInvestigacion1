namespace Investigacion1.Features.Usuarios;

public class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Role { get; set; } = Usuarios.Role.Subscription_L1;
}