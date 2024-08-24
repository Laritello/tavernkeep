using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Ancestries;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;
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
				.SeedAncestries()
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
				var conditions = JsonSerializer.Deserialize<List<ConditionMetadata>>(json, options) ?? [];

				context.Set<ConditionMetadata>().AddRange(conditions);
			}

			context.SaveChanges();

			return context;
		}

		private static SessionContext SeedAncestries(this SessionContext context)
		{
			if (!context.Set<AncestryMetadata>().Any())
			{
				var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Ancestries.en-UK.json");
				using var sr = new StreamReader(filePath);

				var json = sr.ReadToEnd();
				var conditions = JsonSerializer.Deserialize<List<AncestryMetadata>>(json, options) ?? [];

				context.Set<AncestryMetadata>().AddRange(conditions);
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
					Advancements =
					[
						new AbilityBoostAdvancement()
						{
							Level = 1,
							Possible = [AbilityType.Dexterity],
							Selected = AbilityType.Dexterity
						},
						new AbilityBoostAdvancement()
						{
							Level = 1,
							Possible = [AbilityType.Intelligence],
							Selected = AbilityType.Intelligence
						},
						new AbilityBoostAdvancement()
						{
							Level = 1,
							Possible = [AbilityType.Strength, AbilityType.Constitution, AbilityType.Wisdom, AbilityType.Charisma],
							Selected = AbilityType.Wisdom
						},
						new AbilityFlawAdvancement()
						{
							Level = 1,
							Possible = [AbilityType.Constitution],
							Selected = AbilityType.Constitution
						}
					]
				};

				character.Build.Background = new()
				{
					Name = "Scholar (Religion)",
					Advancements =
					[
						new AbilityBoostAdvancement()
						{
							Level = 1,
							Possible = [AbilityType.Intelligence, AbilityType.Wisdom],
							Selected = AbilityType.Wisdom
						}
					]
				};

				character.Build.Class = new()
				{
					Name = "Cleric",
					Advancements =
					[
						new KeyAbilityAdvancement()
						{
							Level = 1,
							Possible = [AbilityType.Wisdom],
							Selected = AbilityType.Wisdom
						},
						new SkillIncreaseAdvancement()
						{
							Level = 1,
							Possible = [SkillType.Religion],
							Selected = SkillType.Religion
						},
						new SkillIncreaseAdvancement()
						{
							Level = 1,
							IsFree = true,
							Selected = SkillType.Medicine,
						},
						new IntelligenceBasedSkillIncreaseAdvancement()
						{
							Level = 1,
							BaseAmount = 2,
							Advancements =
							[
								new SkillIncreaseAdvancement()
								{
									IsFree = true,
									Selected = SkillType.Arcana,
								},
								new SkillIncreaseAdvancement()
								{
									IsFree = true,
									Selected = SkillType.Crafting,
								}
							]
						},
						new PerceptionProficiencyAdvancement()
						{
							Proficiency = Proficiency.Trained
						}
					]
				};

				context.Set<Character>().Add(character);
			}

			context.SaveChanges();

			return context;
		}
	}
}
