using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Readora.API.Middleware;

public class GlobalExceptionMiddleware
{
	private readonly RequestDelegate _next;

	private readonly ILogger<GlobalExceptionMiddleware> _logger;

	public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception exception)
		{
			_logger.LogError(exception, "An unhandled exception occurred during the request.");
			await HandleExceptionAsync(context, exception);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = 500;
		var exceptionMessage = exception.InnerException?.Message ?? exception.Message;
		var response = new
		{
			success = false,
			message = "An unexpected error occurred.",
			errors = new string[1] { exceptionMessage }
		};
		JsonSerializerOptions options = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
		return context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
	}
}
