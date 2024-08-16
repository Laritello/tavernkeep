using System.Text.Json;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Entities;
using Tavernkeep.Infrastructure.Data.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Tavernkeep.Infrastructure.Data.Extensions
{
	public static class EntityFrameworkSeedExtensions
	{
		/// <summary>
		/// Seeds data (ancestries, classes, etc.) into the database.
		/// </summary>
		/// <param name="provider">The <see cref="IServiceProvider"/> that contains the database context.</param>
		/// <returns>The <see cref="IServiceProvider"/> so that additional calls can be chained.</returns>
		public static IServiceProvider SeedData(this IServiceProvider provider)
		{
			var scope = provider.CreateScope();
			var context = scope.ServiceProvider.GetRequiredService<SessionContext>();

			context
				.SeedUsers()
				.SeedConditions();
			context.SaveChanges();

			return provider;
		}

		private static SessionContext SeedUsers(this SessionContext context)
		{
			if (!context.Set<User>().Any())
			{
				context.Set<User>().Add(new("admin", "admin", UserRole.Master)
				{
					Id = Guid.NewGuid()
				});
			}

			return context;
		}

		private static SessionContext SeedConditions(this SessionContext context) 
		{
			if (!context.Set<ConditionMetadata>().Any())
			{
				var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Conditions.en-UK.json");
				using var sr = new StreamReader(filePath);

				var json = sr.ReadToEnd();
				var conditions = JsonSerializer.Deserialize<List<ConditionMetadata>>(json) ?? [];

				context.Set<ConditionMetadata>().AddRange(conditions);
			}

			return context;
		}
	}
}
