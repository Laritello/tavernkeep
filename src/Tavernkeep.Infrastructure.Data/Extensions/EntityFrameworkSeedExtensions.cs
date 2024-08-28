using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityBoost;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityFlaw;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.SkillIncrease;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Values;
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
				//var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Ancestries.en-UK.json");
				//using var sr = new StreamReader(filePath);

				//var json = sr.ReadToEnd();
				//var ancestries = JsonSerializer.Deserialize<List<AncestryTemplate>>(json, options) ?? [];

				//context.Set<AncestryTemplate>().AddRange(ancestries);

				context.Set<AncestryTemplate>().Add(new()
				{
					Id = "pf:ancestry:dwarf",
					Name = "Dwarf",
					Attributes =
					[
						new FixedAbilityBoostAttribute()
						{
							Id = Guid.Parse("2b802ea4-3b4e-4804-a269-a63d8e03c53f"),
							Level = 1,
							Selected = [AbilityType.Constitution],
						},
						new FixedAbilityBoostAttribute()
						{
							Id = Guid.Parse("617c1d5c-9be1-47f0-8005-838d5442eff1"),
							Level = 1,
							Selected = [AbilityType.Wisdom],
						},
						new VariableAbilityBoostAttribute()
						{
							Id = Guid.Parse("7f92d952-fee6-4d2b-b455-831b7c75bc49"),
							Level = 1,
							Amount = 1,
							Possible = [.. Enum.GetValues<AbilityType>()]
						},
						new FixedAbilityFlawAttribute()
						{
							Id = Guid.Parse("a157800d-6b9d-4385-aa84-519bc16f9968"),
							Level = 1,
							Selected = [AbilityType.Charisma],
						}
					]
				});
			}

			context.SaveChanges();

			return context;
		}

		private static SessionContext SeedClasses(this SessionContext context)
		{
			if (!context.Set<ClassTemplate>().Any())
			{
				//var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Classes.en-UK.json");
				//using var sr = new StreamReader(filePath);

				//var json = sr.ReadToEnd();
				//var classes = JsonSerializer.Deserialize<List<ClassTemplate>>(json, options) ?? [];

				//context.Set<ClassTemplate>().AddRange(classes);

				context.Set<ClassTemplate>().Add(new()
				{
					Id = "pf:class:bard",
					Name = "Bard",
					SkillBaseAmount = 4,
					Attributes =
					[
						new FixedKeyAbilityAttribute()
						{
							Id = Guid.Parse("ff40c1d9-bb03-4c36-8871-6d246ef02d98"),
							Level = 1,
							Selected = [AbilityType.Charisma],
						},
						new FixedSkillIncreaseAttribute()
						{
							Id = Guid.Parse("720617c0-ab4d-4d2d-aeca-c8c128a54c16"),
							Level = 1,
							Selected = [SkillType.Occultism],
							MaxProficiency = Proficiency.Trained,
						},
						new FixedSkillIncreaseAttribute()
						{
							Id = Guid.Parse("a8ed7493-9f09-41f8-a847-e0b026cf11f4"),
							Level = 1,
							Selected = [SkillType.Performance],
							MaxProficiency = Proficiency.Trained,
						},
						new BonusSkillIncreaseAttribute()
						{
							Id = Guid.Parse("2d6dc40a-f7fb-4eb9-ba0a-fd33af64ce55"),
							Level = 1,
							Amount = 4,
							MaxProficiency = Proficiency.Trained,
						}
					]
				});
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
					},
					Snapshot = new()
					{
						General = new()
						{
							Values =
							[
								new AbilityBoostValue()
								{
									Id = Guid.Parse("3f54e631-7fba-416d-8803-732ae15cc1da"),
									Selected = [AbilityType.Dexterity, AbilityType.Intelligence, AbilityType.Constitution, AbilityType.Wisdom],
								},
							],
						},
						Ancestry = new()
						{
							Id = "pf:ancestry:dwarf",
							Values =
							[
								new AbilityBoostValue()
								{
									Id = Guid.Parse("2b802ea4-3b4e-4804-a269-a63d8e03c53f"),
									Selected = [AbilityType.Constitution],
								},
								new AbilityBoostValue()
								{
									Id = Guid.Parse("617c1d5c-9be1-47f0-8005-838d5442eff1"),
									Selected = [AbilityType.Wisdom],
								},
								new AbilityBoostValue()
								{
									Id = Guid.Parse("7f92d952-fee6-4d2b-b455-831b7c75bc49"),
									Selected = [AbilityType.Strength],
								},
								new AbilityFlawValue()
								{
									Id = Guid.Parse("a157800d-6b9d-4385-aa84-519bc16f9968"),
									Selected = [AbilityType.Charisma],
								},
							]
						},
						Class = new()
						{
							Id = "pf:class:bard",
							Values =
							[
								new AbilityBoostValue()
								{
									Id = Guid.Parse("ff40c1d9-bb03-4c36-8871-6d246ef02d98"),
									Selected = [AbilityType.Charisma],
								},
								new SkillIncreaseValue()
								{
									Id = Guid.Parse("720617c0-ab4d-4d2d-aeca-c8c128a54c16"),
									Selected = [SkillType.Occultism],
								},
								new SkillIncreaseValue()
								{
									Id = Guid.Parse("a8ed7493-9f09-41f8-a847-e0b026cf11f4"),
									Selected = [SkillType.Performance],
								},
								new SkillIncreaseValue()
								{
									Id = Guid.Parse("2d6dc40a-f7fb-4eb9-ba0a-fd33af64ce55"),
									Selected = [SkillType.Diplomacy, SkillType.Society, SkillType.Arcana],
								}
							]
						},
					}
				};

				context.Set<Character>().Add(character);
			}

			context.SaveChanges();

			return context;
		}
	}
}
