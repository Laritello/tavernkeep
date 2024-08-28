﻿using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Entities.Templates;
using Tavernkeep.Infrastructure.Data.Context;

namespace Tavernkeep.Infrastructure.Data.Extensions
{
	public static class EntityFrameworkSeedExtensions
	{
		private static readonly JsonSerializerOptions options = new()
		{
			Converters = { new JsonStringEnumConverter() }
		};

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
				.SeedConditions()
				.SeedAncestries()
				.SeedClasses()
				.SeedCharacter();

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

			context.SaveChanges();

			return context;
		}

		private static SessionContext SeedConditions(this SessionContext context)
		{
			if (!context.Set<ConditionTemplate>().Any())
			{
				var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Conditions.en-UK.json");
				using var sr = new StreamReader(filePath);

				var json = sr.ReadToEnd();
				var conditions = JsonSerializer.Deserialize<List<ConditionTemplate>>(json, options) ?? [];

				context.Set<ConditionTemplate>().AddRange(conditions);
			}

			context.SaveChanges();

			return context;
		}

		private static SessionContext SeedAncestries(this SessionContext context)
		{
			if (!context.Set<AncestryTemplate>().Any())
			{
				var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Ancestries.en-UK.json");
				using var sr = new StreamReader(filePath);

				var json = sr.ReadToEnd();
				var ancestries = JsonSerializer.Deserialize<List<AncestryTemplate>>(json, options) ?? [];

				context.Set<AncestryTemplate>().AddRange(ancestries);
			}

			context.SaveChanges();

			return context;
		}

		private static SessionContext SeedClasses(this SessionContext context)
		{
			if (!context.Set<ClassTemplate>().Any())
			{
				var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Classes.en-UK.json");
				using var sr = new StreamReader(filePath);

				var json = sr.ReadToEnd();
				var classes = JsonSerializer.Deserialize<List<ClassTemplate>>(json, options) ?? [];

				context.Set<ClassTemplate>().AddRange(classes);
			}

			context.SaveChanges();

			return context;
		}

		private static SessionContext SeedCharacter(this SessionContext context)
		{
			if (!context.Set<Character>().Any())
			{
				var character = new Character()
				{
					Id = Guid.NewGuid(),
					Owner = context.Set<User>().First(),
					Name = "Soveliss",
					Build = new()
					{
						Level = 1
					}
				};

				context.Set<Character>().Add(character);
			}

			context.SaveChanges();

			return context;
		}
	}
}
