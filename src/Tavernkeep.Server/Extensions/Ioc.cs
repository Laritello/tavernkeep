using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Context;
using Tavernkeep.Infrastructure.Repositories;
using Tavernkeep.Infrastructure.Utility;
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
                    ValidateAudience= false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key ?? string.Empty)),
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
