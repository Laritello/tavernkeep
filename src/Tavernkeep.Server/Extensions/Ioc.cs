using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.Services;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Services;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Repositories;
using Tavernkeep.Infrastructure.Data.Utility;
using Tavernkeep.Infrastructure.Notifications.Storage;
using Tavernkeep.Server.Exceptions.Handlers;
using Tavernkeep.Shared.Options;

namespace Tavernkeep.Server.Extensions
{
	/// <summary>
	/// Provides convenient way to implement dependency injection.
	/// </summary>
	public static class Ioc
	{
		/// <summary>
		/// Initializes required services for authentication and authorization.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
		/// <param name="key">The key used to sign JWT.</param>
		/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
		public static IServiceCollection AddSecurity(this IServiceCollection services, string? key)
		{
			services.AddTransient<IAuthTokenService, AuthTokenService>();

			services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.TokenValidationParameters = new()
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key ?? string.Empty)),
				};

				o.Events = new JwtBearerEvents
				{
					OnMessageReceived = context =>
					{
						var accessToken = context.Request.Query["access_token"];

						// If the request is for our hub...
						var path = context.HttpContext.Request.Path;
						if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/api/hubs"))
						{
							// Read the token out of the query string
							context.Token = accessToken;
						}
						return Task.CompletedTask;
					}
				};
			});

			services.AddAuthorization();
			return services;
		}

		/// <summary>
		/// Initializes database context and configures repository dependencies.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
		/// <param name="options">The launch options for application.</param>
		/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
		public static IServiceCollection AddDatabaseContext(this IServiceCollection services, LaunchOptions options)
		{
			var connectionString = DatabaseContextUtility.GetConnectionString(options.CampaignName);
			services.AddDbContext<SessionContext>(options => options.UseSqlite(connectionString));

			services.AddScoped<IUserRepository, UserEFRepository>();
			services.AddScoped<ICharacterRepository, CharacterEFRepository>();
			services.AddScoped<IMessageRepository, MessageEFRepository>();
			services.AddScoped<IRefreshTokenRepository, RefreshTokenEFRepository>();
			services.AddScoped<IConditionMetadataRepository, ConditionMetadataEFRepository>();
			services.AddScoped<IPortraitRepository, PortraitEFRepository>();
			services.AddScoped<IEncounterRepository, EncounterEFRepository>();

			return services;
		}

		/// <summary>
		/// Adds excpetion handlers to the service collection.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> to add the error middleware to.</param>
		/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
		public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
		{
			services.AddExceptionHandler<BusinessLogicExceptionHandler>();
			services.AddExceptionHandler<AuthorizationExceptionHandler>();
			services.AddExceptionHandler<GenericExceptionHandler>();

			return services;
		}

		/// <summary>
		/// Adds application services to the service collection.
		/// </summary>
		/// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
		/// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddSingleton<IDiceService, DiceService>();
			services.AddSingleton<INotificationService, NotificationService>();

			services.AddScoped<ICharacterService, CharacterService>();
			services.AddScoped<IPortaitService, PortraitService>();
			services.AddScoped<IEncounterService, EncounterService>();

			services.AddSingleton<IUserConnectionStorage<Guid>, UserConnectionStorage<Guid>>();

			services.AddHostedService(sp => (NotificationService)sp.GetRequiredService<INotificationService>());
			services.AddHostedService<RefreshTokenService>();

			return services;
		}

		/// <summary>
		/// Applies migrations to the database.
		/// </summary>
		/// <param name="provider">The <see cref="IServiceProvider"/> that contains the database context.</param>
		/// <returns>The <see cref="IServiceProvider"/> so that additional calls can be chained.</returns>
		public static IServiceProvider ApplyDatabaseMigrations(this IServiceProvider provider)
		{
			var scope = provider.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<SessionContext>();
			context.Database.Migrate();

			return provider;
		}
	}
}
