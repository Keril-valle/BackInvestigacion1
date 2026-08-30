using System.Security.Claims;

namespace Investigacion1.Features.Usuarios;

public static class ClaimsExtensions
{
    public static string? GetEmail(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email)
            ?? user.FindFirstValue(ClaimTypes.Email);
    }
}
