using Microsoft.AspNetCore.Mvc;
using Readora.Application.Common;

namespace Readora.API.Extensions;

public static class ResultExtensions
{
	public static IActionResult ToActionResult(this Result result)
	{
		if (result.IsSuccess)
		{
			return new OkResult();
		}
		return CreateErrorResponse(result.Error);
	}

	public static IActionResult ToActionResult<T>(this Result<T> result)
	{
		if (result.IsSuccess)
		{
			return new OkObjectResult(result.Value);
		}
		return CreateErrorResponse(result.Error);
	}

	private static IActionResult CreateErrorResponse(Error error)
	{
		ProblemDetails problemDetails = new ProblemDetails
		{
			Title = GetTitle(error.Type),
			Status = GetStatusCode(error.Type),
			Detail = error.Message,
			Extensions = { 
			{
				"code",
				(object)error.Code
			} }
		};
		return new ObjectResult(problemDetails)
		{
			StatusCode = problemDetails.Status
		};
	}

	private static int GetStatusCode(ErrorType errorType)
	{
		return errorType switch
		{
			ErrorType.Validation => 400, 
			ErrorType.NotFound => 404, 
			ErrorType.Conflict => 409, 
			ErrorType.Unauthorized => 401, 
			ErrorType.Forbidden => 403,
			_ => 500, 
		};
	}

	private static string GetTitle(ErrorType errorType)
	{
		return errorType switch
		{
			ErrorType.Validation => "Bad Request", 
			ErrorType.NotFound => "Not Found", 
			ErrorType.Conflict => "Conflict", 
			ErrorType.Unauthorized => "Unauthorized", 
			ErrorType.Forbidden => "Forbidden",
			_ => "Server Error", 
		};
	}
}
