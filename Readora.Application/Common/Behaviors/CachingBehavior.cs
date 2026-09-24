using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Readora.Application.Common.Caching;

namespace Readora.Application.Common.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
	private readonly IDistributedCache _cache;

	public CachingBehavior(IDistributedCache cache)
	{
		_cache = cache;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		if (!(request is ICacheableQuery cacheableQuery))
		{
			return await next();
		}
		string cachedResponse = await _cache.GetStringAsync(cacheableQuery.CacheKey, cancellationToken);
		if (!string.IsNullOrEmpty(cachedResponse))
		{
			return JsonSerializer.Deserialize<TResponse>(cachedResponse);
		}
		var response = await next();
		if (response != null)
		{
			await DistributedCacheExtensions.SetStringAsync(options: new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = (cacheableQuery.Expiration ?? TimeSpan.FromMinutes(10L))
			}, cache: _cache, key: cacheableQuery.CacheKey, value: JsonSerializer.Serialize(response), token: cancellationToken);
		}
		return response;
	}
}
