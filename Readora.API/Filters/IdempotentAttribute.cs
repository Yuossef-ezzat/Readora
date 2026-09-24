using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace Readora.API.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IdempotentAttribute : Attribute, IAsyncActionFilter, IFilterMetadata
{
	private const string IdempotencyHeader = "X-Idempotency-Key";

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		if (!context.HttpContext.Request.Headers.TryGetValue("X-Idempotency-Key", out var idempotencyKey))
		{
			await next();
			return;
		}
		IDistributedCache cache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
		string cacheKey = $"Idempotency_{idempotencyKey}";
		string cachedResult = await cache.GetStringAsync(cacheKey);
		if (!string.IsNullOrEmpty(cachedResult))
		{
			IdempotencyCachedResult result = JsonSerializer.Deserialize<IdempotencyCachedResult>(cachedResult);
			if (result != null)
			{
				context.Result = new ObjectResult(result.Value)
				{
					StatusCode = result.StatusCode
				};
				return;
			}
		}
		IActionResult result2 = (await next()).Result;
		if (result2 is ObjectResult objectResult)
		{
			IdempotencyCachedResult resultToCache = new IdempotencyCachedResult
			{
				StatusCode = (objectResult.StatusCode ?? 200),
				Value = objectResult.Value
			};
			await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(resultToCache), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
			});
		}
	}
}
