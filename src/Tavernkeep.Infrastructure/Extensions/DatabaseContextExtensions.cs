﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Infrastructure.Context;
using Tavernkeep.Infrastructure.Repositories;
using Tavernkeep.Infrastructure.Utility;
using Tavernkeep.Shared.Options;

namespace Tavernkeep.Infrastructure.Extensions
{
    public static class DatabaseContextExtensions
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, LaunchOptions options)
        {
            var connectionString = DatabaseContextUtility.GetConnectionString(options.CampaignName);
            services.AddDbContext<SessionContext>(options => options.UseSqlite(connectionString));

            services.AddScoped<IUserRepository, UserEFRepository>();
            services.AddScoped<ICharacterRepository, CharacterEFRepository>();

            return services;
        }

        public static void ApplyDatabaseMigrations(this IServiceProvider services)
        {
            var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SessionContext>();
            context.Database.Migrate();
        }
    }
}
