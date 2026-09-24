using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Readora.Application.Common.Behaviors;

namespace Readora.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
			cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CacheInvalidationBehavior<,>));
		});
		return services;
	}
}
