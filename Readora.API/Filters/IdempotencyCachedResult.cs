namespace Readora.API.Filters;

public class IdempotencyCachedResult
{
	public int StatusCode { get; set; }

	public object? Value { get; set; }
}
