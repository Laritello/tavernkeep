using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
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

		private static SessionContext SeedCharacter(this SessionContext context)
		{
			if (!context.Set<Character>().Any())
			{
				var character = new Character()
				{
					Id = Guid.NewGuid(),
					Owner = context.Set<User>().First(),
					Name = "Roland",
					Ancestry = "Human",
					Class = "Psychic",
					Level = 6
				};

				character.Health.Max = 56;
				character.Health.Current = 13;
				character.Health.Temporary = 5;

				character.Strength.Score = 10;
				character.Dexterity.Score = 12;
				character.Constitution.Score = 14;
				character.Intelligence.Score = 18;
				character.Wisdom.Score = 12;
				character.Charisma.Score = 19;

				character.Fortitude.Proficiency = Proficiency.Trained;
				character.Reflex.Proficiency = Proficiency.Expert;
				character.Will.Proficiency = Proficiency.Expert;

				character.Perception.Proficiency = Proficiency.Expert;

				character.Acrobatics.Proficiency = Proficiency.Trained;
				character.Arcana.Proficiency = Proficiency.Trained;
				character.Deception.Proficiency = Proficiency.Trained;
				character.Diplomacy.Proficiency = Proficiency.Expert;
				character.Intimidation.Proficiency = Proficiency.Trained;
				character.Occultism.Proficiency = Proficiency.Expert;
				character.Performance.Proficiency = Proficiency.Trained;
				character.Religion.Proficiency = Proficiency.Trained;
				character.Society.Proficiency = Proficiency.Trained;
				character.Stealth.Proficiency = Proficiency.Trained;

				context.Set<Character>().Add(character);
				context.SaveChanges();

				context.Set<User>().First().ActiveCharacter = character;
				context.SaveChanges();
			}

			return context;
		}
	}
}
