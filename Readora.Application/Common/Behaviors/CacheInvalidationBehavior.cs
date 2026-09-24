using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Readora.Application.Common.Caching;

namespace Readora.Application.Common.Behaviors;

public class CacheInvalidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
	private readonly IDistributedCache _cache;

	public CacheInvalidationBehavior(IDistributedCache cache)
	{
		_cache = cache;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		var response = await next();
		if (request is ICacheInvalidatorCommand cacheInvalidator)
		{
			string[] cacheKeys = cacheInvalidator.CacheKeys;
			foreach (string key in cacheKeys)
			{
				await _cache.RemoveAsync(key, cancellationToken);
			}
		}
		return response;
	}
}
