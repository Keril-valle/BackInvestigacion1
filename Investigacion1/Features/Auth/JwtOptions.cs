namespace Investigacion1.Features.Auth;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Secret { get; set; } = "miClaveSecreta1234567890abcdefghijklmnopqrstuvwxyz";

    public string Issuer { get; set; } = "Investigacion1";

    public string Audience { get; set; } = "Investigacion1";

    public int ExpirationHours { get; set; } = 1;

    public int RefreshTokenExpirationDays { get; set; } = 14;
}