using Amazon.Runtime.Internal.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Readora.API.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class IdempotentAttribute : Attribute, IAsyncActionFilter, IFilterMetadata
{
	private const string IdempotencyHeader = "X-Idempotency-Key";
	private const string InProgress = "InProgress";

	public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
	{
		if (!context.HttpContext.Request.Headers.TryGetValue(IdempotencyHeader, out var idempotencyKey) || string.IsNullOrEmpty(idempotencyKey))
		{
			context.Result = new BadRequestObjectResult($"Missing or empty {IdempotencyHeader} header.");
			return;
		}

		var cacheKey = $"Idempotency_{idempotencyKey}";
		var cache = context.HttpContext.RequestServices.GetRequiredService<IDistributedCache>();
		var cachedResult = await cache.GetStringAsync(cacheKey);

		if (!string.IsNullOrEmpty(cachedResult))
		{
			if (cachedResult == InProgress)
			{
				context.Result = new StatusCodeResult(409); // Conflict
				return;
			}
			var result = JsonSerializer.Deserialize<IdempotencyCachedResult>(cachedResult);
			if (result != null)
			{
				context.Result = new ObjectResult(result.Value)
				{
					StatusCode = result.StatusCode
				};
				return;
			}
		}
		else
		{
			await cache.SetStringAsync(cacheKey, InProgress, new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10)
			});
			var result2 = await next();
			if (result2.Result is ObjectResult objectResult)
			{
				if (objectResult.StatusCode >= 200 && objectResult.StatusCode < 300)
				{
					var resultToCache = new IdempotencyCachedResult
					{
						StatusCode = (objectResult.StatusCode ?? 200),
						Value = objectResult.Value
					};
					await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(resultToCache), new DistributedCacheEntryOptions
					{
						AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
					});
				}
                else
                {
                    await cache.RemoveAsync(cacheKey);
                }
            }

		}
	}
}
