using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Auth.Refresh;

public class RefreshCommand
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}