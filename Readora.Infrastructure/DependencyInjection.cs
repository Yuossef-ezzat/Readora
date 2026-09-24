using System;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Readora.Application.Interfaces.Persistence;
using Readora.Application.Interfaces.Services;
using Readora.Domain.Entities;
using Readora.Infrastructure.Authentication;
using Readora.Infrastructure.Persistence;
using Readora.Infrastructure.Persistence.Repositories;
using Readora.Infrastructure.Services;

namespace Readora.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		string connectionString = configuration.GetConnectionString("DefaultConnection") 
			?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseNpgsql(connectionString));

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
		services.AddScoped<IEmailService, EmailService>();
		services.AddScoped<IOtpService, OtpService>();
		services.AddScoped<ITokenService, TokenService>();
		services.AddScoped<IAuthService, AuthService>();
        
		services.AddIdentityCore<ApplicationUser>(options =>
		{
			options.Password.RequireDigit = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireUppercase = true;
			options.Password.RequiredLength = 8;
		})
		.AddRoles<IdentityRole<int>>()
		.AddEntityFrameworkStores<ApplicationDbContext>()
		.AddDefaultTokenProviders();
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        JwtSettings jwtSettings = new JwtSettings();
		configuration.Bind("JwtSettings", jwtSettings);
		services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtSettings.Issuer,
				ValidAudience = jwtSettings.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
			};
			options.Events = new JwtBearerEvents
			{
				OnMessageReceived = context =>
				{
					var accessToken = context.Request.Query["access_token"];
					var path = context.HttpContext.Request.Path;
					if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs/notifications"))
					{
						context.Token = accessToken;
					}
					return Task.CompletedTask;
				}
			};
		});

		services.AddDistributedMemoryCache();
		services.AddScoped<IStorageService, SupabaseStorageService>();
		services.Configure<SupabaseS3Settings>(configuration.GetSection("SupabaseS3Settings"));
        
		var supabaseSettings = configuration.GetSection("SupabaseS3Settings").Get<SupabaseS3Settings>() ?? new SupabaseS3Settings();
		var s3Config = new AmazonS3Config
		{
			ServiceURL = supabaseSettings.Endpoint,
			ForcePathStyle = true
		};
		var awsCredentials = new BasicAWSCredentials(supabaseSettings.AccessKey, supabaseSettings.SecretKey);
		services.AddSingleton<IAmazonS3>(new AmazonS3Client(awsCredentials, s3Config));

		services.AddHangfire(config => config
			.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
			.UseSimpleAssemblyNameTypeSerializer()
			.UseRecommendedSerializerSettings()
			.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(connectionString)));
            
		services.AddHangfireServer();
        
		return services;
	}
}

