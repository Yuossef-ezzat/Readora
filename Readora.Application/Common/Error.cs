namespace Readora.Application.Common;

public sealed record Error(string Code, string Message, ErrorType Type)
{
	public static readonly Error None = new Error(string.Empty, string.Empty, ErrorType.Failure);

	public static readonly Error NullValue = new Error("Error.NullValue", "Null value was provided", ErrorType.Failure);

	public static Error NotFound(string code, string message)
	{
		return new Error(code, message, ErrorType.NotFound);
	}

	public static Error Validation(string code, string message)
	{
		return new Error(code, message, ErrorType.Validation);
	}

	public static Error Conflict(string code, string message)
	{
		return new Error(code, message, ErrorType.Conflict);
	}

	public static Error Failure(string code, string message)
	{
		return new Error(code, message, ErrorType.Failure);
	}

	public static Error Unauthorized(string code, string message)
	{
		return new Error(code, message, ErrorType.Unauthorized);
	}

	public static Error Forbidden(string code, string message)
	{
		return new Error(code, message, ErrorType.Forbidden);
	}
}
