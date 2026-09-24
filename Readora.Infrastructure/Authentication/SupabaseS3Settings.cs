namespace Readora.Infrastructure.Authentication;

public class SupabaseS3Settings
{
	public const string SectionName = "SupabaseS3Settings";

	public string AccessKey { get; set; } = string.Empty;

	public string SecretKey { get; set; } = string.Empty;

	public string Endpoint { get; set; } = string.Empty;

	public string Region { get; set; } = "eu-west-1";

	public string BucketName { get; set; } = string.Empty;
}
