﻿using Tavernkeep.Core.Contracts.Enums;
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

			character.Health.Max = 56;
			character.Health.Current = 13;
			character.Health.Temporary = 5;

			character.Abilities =
			[
				new("Strength", 10) { Owner = character },
				new("Dexterity", 12) { Owner = character },
				new("Constitution", 14) { Owner = character },
				new("Intelligence", 18) { Owner = character },
				new("Wisdom", 12) { Owner = character },
				new("Charisma", 19) { Owner = character }
			];

			character.SavingThrows =
			[
				new("Fortitude", Proficiency.Trained)
				{
					Owner = character,
					Ability = character.Abilities["Constitution"]
				},
				new("Reflex", Proficiency.Expert)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},
				new("Will", Proficiency.Expert)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				}
			];

			character.Skills =
			[
				new("Acrobatics", Proficiency.Trained)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},
				new("Arcana", Proficiency.Trained)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Athletics", Proficiency.Untrained)
				{
					Owner = character,
					Ability = character.Abilities["Strength"]
				},
				new("Crafting", Proficiency.Untrained)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Deception", Proficiency.Trained)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Diplomacy", Proficiency.Expert)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Intimidation", Proficiency.Trained)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Medicine", Proficiency.Untrained)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Nature", Proficiency.Untrained)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Occultism", Proficiency.Expert)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Performance", Proficiency.Expert)
				{
					Owner = character,
					Ability = character.Abilities["Charisma"]
				},
				new("Religion", Proficiency.Trained)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Society", Proficiency.Trained)
				{
					Owner = character,
					Ability = character.Abilities["Intelligence"]
				},
				new("Stealth", Proficiency.Trained)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},
				new("Survival", Proficiency.Untrained)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
				new("Thievery", Proficiency.Untrained)
				{
					Owner = character,
					Ability = character.Abilities["Dexterity"]
				},

				new("Perception", Proficiency.Expert)
				{
					Owner = character,
					Ability = character.Abilities["Wisdom"]
				},
			];

			character.Walk.Base = 30;

			return character;
		}
	}
}
