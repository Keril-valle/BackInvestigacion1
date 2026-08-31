using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Auth.AdminRegister;

public class AdminRegisterCommand
{
    [Required]
    [MinLength(3)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string NumeroLicencia { get; set; } = string.Empty;
}