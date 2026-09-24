using System;
using System.Text;
using System.Threading.RateLimiting;
using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Readora.API.Extensions;
using Readora.API.Hubs;
using Readora.API.Middleware;
using Readora.API.Services;
using Readora.Application;
using Readora.Application.Interfaces.Services;
using Readora.Infrastructure;
using Serilog;
using Serilog.AspNetCore;
using Serilog.Core;
using Serilog.Events;
using Serilog.Settings.Configuration;
using Serilog.Sinks.File;
using Serilog.Sinks.SystemConsole.Themes;

namespace Readora.API;

public class Program
{
	public static void Main(string[] args)
	{
		Env.TraversePath().Load();
		Log.Logger = new LoggerConfiguration()
			.WriteTo.Console()
			.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
			.CreateBootstrapLogger();
		try
		{
			Log.Information("Starting Readora API");
			WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
			builder.Host.UseSerilog((context, services, configuration) => configuration
				.ReadFrom.Configuration(context.Configuration)
				.ReadFrom.Services(services)
				.Enrich.FromLogContext()
				.WriteTo.Console()
				.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day));
			builder.Services.AddControllers();
			builder.Services.AddSignalR();
			builder.Services.AddScoped<INotificationService, SignalRNotificationService>();
			builder.Services.AddRateLimiter((RateLimiterOptions options) =>
			{
				options.RejectionStatusCode = 429;
				options.AddFixedWindowLimiter("fixed", (FixedWindowRateLimiterOptions fixedWindowRateLimiterOptions) =>
				{
					fixedWindowRateLimiterOptions.PermitLimit = 100;
					fixedWindowRateLimiterOptions.Window = TimeSpan.FromMinutes(1L);
					fixedWindowRateLimiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
					fixedWindowRateLimiterOptions.QueueLimit = 2;
				});
			});
			builder.Services.AddApplication();
			builder.Services.AddInfrastructure(builder.Configuration);
			builder.Services.AddOpenApi();
			WebApplication app = builder.Build();
			
			// Seed database
			Readora.Infrastructure.Persistence.DatabaseSeeder.SeedRolesAsync(app.Services).GetAwaiter().GetResult();

			app.UseSerilogRequestLogging();
			app.UseMiddleware<GlobalExceptionMiddleware>();
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
			}
			app.UseHttpsRedirection();
			app.UseRateLimiter();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseBackgroundJobs();
			app.MapControllers();
			app.MapHub<NotificationHub>("/hubs/notifications");
			app.Run();
		}
		catch (Exception ex)
		{
			Log.Fatal(ex, "Application terminated unexpectedly");
		}
		finally
		{
			Log.CloseAndFlush();
		}
	}
}
