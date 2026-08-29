using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Auth.Login;

public class LoginQuery
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}