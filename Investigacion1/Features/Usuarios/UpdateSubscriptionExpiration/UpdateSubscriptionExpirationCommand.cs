using System.ComponentModel.DataAnnotations;

namespace Investigacion1.Features.Usuarios.UpdateSubscriptionExpiration;

public class UpdateSubscriptionExpirationCommand
{
    [Required]
    public DateTime SubscriptionExpirationDate { get; set; }
}
