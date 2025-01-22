using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkepp.Application.Tests
{
	/// <summary>
	/// Quick and dirty character generator
	/// </summary>
	public class CharacterGenerator
	{
		public static Character Generate(Guid? guid = null, User? owner = null)
		{
			var character = new Character()
			{
				Id = guid ?? Guid.NewGuid(),
				Owner = owner ?? new("User", string.Empty, UserRole.Player) { Id = Guid.NewGuid() },
				Name = "Roland Engreen",
				Ancestry = "Human",
				Class = "Psychic",
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

				new("Academia", Proficiency.Trained, SkillType.Lore)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
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

			return character;
		}
	}
}
