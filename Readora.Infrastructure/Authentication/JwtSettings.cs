namespace Readora.Infrastructure.Authentication;

public class JwtSettings
{
	public const string SectionName = "JwtSettings";

	public string SecretKey { get; set; } = string.Empty;

	public int ExpiryMinutes { get; set; } = 60;

	public string Issuer { get; set; } = string.Empty;

	public string Audience { get; set; } = string.Empty;
}
