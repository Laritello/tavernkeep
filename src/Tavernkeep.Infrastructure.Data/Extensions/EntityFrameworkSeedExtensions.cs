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
				.SeedCreatures()
				.SeedCharacter();

			return provider;
		}

		private static SessionContext SeedUsers(this SessionContext context)
		{
			if (!context.Set<User>().Any())
			{
				context.Set<User>().Add(new("admin", "admin", UserRole.Master)
				{
					Id = Guid.Parse("6d3dcfcd-7d87-4b91-8245-c4dba656d2c2")
				});
			}

			context.SaveChanges();

			return context;
		}

		private static SessionContext SeedConditions(this SessionContext context)
		{
			if (!context.Set<ConditionInformation>().Any())
			{
				var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Conditions.en-UK.json");
				using var sr = new StreamReader(filePath);

				var json = sr.ReadToEnd();
				var conditions = JsonSerializer.Deserialize<List<ConditionInformation>>(json, options) ?? [];

				context.Set<ConditionInformation>().AddRange(conditions);
			}

			context.SaveChanges();

			return context;
		}
		private static SessionContext SeedCreatures(this SessionContext context)
		{
			if (!context.Set<Creature>().Any())
			{
				var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Creatures.en-UK.json");
				using var sr = new StreamReader(filePath);

				var json = sr.ReadToEnd();
				var creatures = JsonSerializer.Deserialize<List<Creature>>(json, options) ?? [];

				context.Set<Creature>().AddRange(creatures);
			}

			return context;
		}

		private static SessionContext SeedCharacter(this SessionContext context)
		{
			if (!context.Set<Character>().Any())
			{
				var character = new Character()
				{
					Id = Guid.Parse("3e9b543b-6d90-4a3b-9072-d6afc3726531"),
					Owner = context.Set<User>().First(),
					Name = "Roland Engreen",
					Level = 6,
				};

				character.Ancestry = new("Half-Elf", 6)
				{
					Owner = character,
				};

				character.Class = new("Psychic", 8)
				{
					Owner = character
				};

				character.Abilities =
				[
					new("Strength", 10) { Owner = character },
					new("Dexterity", 12) { Owner = character },
					new("Constitution", 14) { Owner = character },
					new("Intelligence", 18) { Owner = character },
					new("Wisdom", 12) { Owner = character },
					new("Charisma", 19) { Owner = character }
				];

				character.Skills =
				[
					new("Acrobatics", Proficiency.Trained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Dexterity"]
					},
					new("Arcana", Proficiency.Trained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Intelligence"]
					},
					new("Athletics", Proficiency.Untrained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Strength"]
					},
					new("Crafting", Proficiency.Untrained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Intelligence"]
					},
					new("Deception", Proficiency.Trained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Charisma"]
					},
					new("Diplomacy", Proficiency.Expert, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Charisma"]
					},
					new("Intimidation", Proficiency.Trained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Charisma"]
					},
					new("Medicine", Proficiency.Untrained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Wisdom"]
					},
					new("Nature", Proficiency.Untrained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Wisdom"]
					},
					new("Occultism", Proficiency.Expert, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Intelligence"]
					},
					new("Performance", Proficiency.Expert, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Charisma"]
					},
					new("Religion", Proficiency.Trained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Wisdom"]
					},
					new("Society", Proficiency.Trained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Intelligence"]
					},
					new("Stealth", Proficiency.Trained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Dexterity"]
					},
					new("Survival", Proficiency.Untrained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Wisdom"]
					},
					new("Thievery", Proficiency.Untrained, SkillType.Basic)
					{
						Owner = character,
						Ability = character.Abilities["Dexterity"]
					},

					new("Fortitude", Proficiency.Trained, SkillType.SavingThrow)
					{
						Owner = character,
						Ability = character.Abilities["Constitution"]
					},
					new("Reflex", Proficiency.Expert, SkillType.SavingThrow)
					{
						Owner = character,
						Ability = character.Abilities["Dexterity"]
					},
					new("Will", Proficiency.Expert, SkillType.SavingThrow)
					{
						Owner = character,
						Ability = character.Abilities["Wisdom"]
					},

					new("Perception", Proficiency.Expert, SkillType.Perception)
					{
						Owner = character,
						Ability = character.Abilities["Wisdom"]
					},
				];

				character.Health = new()
				{
					Owner = character,
					Current = 13,
					Temporary = 5
				};

				character.Walk.Base = 30;

				context.Set<Character>().Add(character);
				context.SaveChanges();

				context.Set<User>().First().ActiveCharacter = character;
				context.SaveChanges();
			}

			return context;
		}
	}
}
