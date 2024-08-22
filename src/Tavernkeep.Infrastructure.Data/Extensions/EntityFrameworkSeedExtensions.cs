using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Infrastructure.Data.Context;

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
			if (!context.Set<ConditionMetadata>().Any())
			{
				var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Conditions.en-UK.json");
				using var sr = new StreamReader(filePath);

				var json = sr.ReadToEnd();
				var conditions = JsonSerializer.Deserialize<List<ConditionMetadata>>(json) ?? [];

				context.Set<ConditionMetadata>().AddRange(conditions);
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

				character.Build.Ancestry = new()
				{
					Name = "Elf",
					Progression =
					[
						new(1)
					{
						Level = 1,
						Advancements =
						[
							new AbilityBoostAdvancement()
							{
								Possible = [AbilityType.Dexterity],
								Selected = AbilityType.Dexterity
							},
							new AbilityBoostAdvancement()
							{
								Possible = [AbilityType.Intelligence],
								Selected = AbilityType.Intelligence
							},
							new AbilityBoostAdvancement()
							{
								Possible = [AbilityType.Strength, AbilityType.Constitution, AbilityType.Wisdom, AbilityType.Charisma],
								Selected = AbilityType.Wisdom
							},
							new AbilityFlawAdvancement(){
								Possible = [AbilityType.Constitution],
								Selected = AbilityType.Constitution
							}
						]
					},
				]
				};

				character.Build.Background = new()
				{
					Name = "Scholar (Religion)",
					Progression =
					[
						new(1)
					{
						Level = 1,
						Advancements =
						[
							new AbilityBoostAdvancement()
							{
								Possible = [AbilityType.Intelligence, AbilityType.Wisdom],
								Selected = AbilityType.Wisdom
							}
						]
					},
				]
				};

				character.Build.Class = new()
				{
					Name = "Cleric",
					Progression =
					[
						new(1)
					{
						Level = 1,
						Advancements =
						[
							new AbilityBoostAdvancement()
							{
								Possible = [AbilityType.Wisdom],
								Selected = AbilityType.Wisdom
							}
						]
					},
				]
				};

				character.Arcana.Proficiency = Proficiency.Trained;
				character.Religion.Proficiency = Proficiency.Expert;
				character.Medicine.Proficiency = Proficiency.Trained;
				character.Diplomacy.Proficiency = Proficiency.Trained;
				character.Arcana.Proficiency = Proficiency.Trained;


				context.Set<Character>().Add(character);
			}

			context.SaveChanges();

			return context;
		}
	}
}
