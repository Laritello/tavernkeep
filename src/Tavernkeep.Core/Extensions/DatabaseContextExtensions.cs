using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tavernkeep.Core.EntityFramework.Context;

namespace Tavernkeep.Core.Extensions
{
    public static class DatabaseContextExtensions
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string[] args)
        {
            var sessionsDirectory = GetSessionsDirectory();
            var databasePath = Path.Combine(sessionsDirectory, $"{args[0]}.db");
            var connectionString = $"Data Source={databasePath};";

            services.AddDbContext<SessionContext>(options => options.UseSqlite(connectionString));

            return services;
        }

        public static void ApplyDatabaseMigrations(this IServiceProvider services)
        {
            var scope = services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<SessionContext>();
            context.Database.Migrate();
        }

        private static string GetSessionsDirectory()
        {
            var workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var sessionDirectory = Path.Combine(workingDirectory, "Sessions");

            // Ensure directory exists
            Directory.CreateDirectory(sessionDirectory);

            return sessionDirectory;
        }
    }
}
