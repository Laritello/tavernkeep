using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkepp.Application.Tests.Utility
{
	// TODO: Generate via other methods
	public static class TempGenerator
	{
		public static Character GenerateCharacter(Guid id)
		{
			var character = new Character()
			{
				Id = id,
				Name = "Soveliss",
				Level = 3
			};

			//character.Build.Ancestry = new()
			//{
			//	Name = "Elf",
			//	Progression =
			//	[
			//		new(1)
			//		{
			//			Level = 1,
			//			Advancements =
			//			[
			//				new AbilityBoostAdvancement()
			//				{
			//					Possible = [AbilityType.Dexterity],
			//					Selected = AbilityType.Dexterity
			//				},
			//				new AbilityBoostAdvancement()
			//				{
			//					Possible = [AbilityType.Intelligence],
			//					Selected = AbilityType.Intelligence
			//				},
			//				new AbilityBoostAdvancement()
			//				{
			//					Possible = [AbilityType.Strength, AbilityType.Constitution, AbilityType.Wisdom, AbilityType.Charisma],
			//					Selected = AbilityType.Strength
			//				},
			//				new AbilityFlawAdvancement(){
			//					Possible = [AbilityType.Constitution],
			//					Selected = AbilityType.Constitution
			//				}
			//			]
			//		},
			//	]
			//};

			//character.Build.Background = new()
			//{
			//	Name = "Scholar (Religion)",
			//	Progression =
			//	[
			//		new(1)
			//		{
			//			Level = 1,
			//			Advancements =
			//			[
			//				new AbilityBoostAdvancement()
			//				{
			//					Possible = [AbilityType.Intelligence, AbilityType.Wisdom],
			//					Selected = AbilityType.Intelligence
			//				},
			//				new AbilityFlawAdvancement(){
			//					Possible = [AbilityType.Strength, AbilityType.Dexterity, AbilityType.Constitution, AbilityType.Intelligence, AbilityType.Wisdom, AbilityType.Charisma],
			//					Selected = AbilityType.Strength
			//				}
			//			]
			//		},
			//	]
			//};

			//character.Build.Class = new()
			//{
			//	Name = "Cleric",
			//	Progression =
			//	[
			//		new(1)
			//		{
			//			Level = 1,
			//			Advancements =
			//			[
			//				new AbilityBoostAdvancement()
			//				{
			//					Possible = [AbilityType.Wisdom],
			//					Selected = AbilityType.Wisdom
			//				}
			//			]
			//		},
			//	]
			//};

			character.Strength.Score = 12;
			character.Dexterity.Score = 10;
			character.Constitution.Score = 16;
			character.Intelligence.Score = 14;
			character.Wisdom.Score = 18;
			character.Charisma.Score = 10;

			character.Arcana.Proficiency = Proficiency.Trained;
			character.Religion.Proficiency = Proficiency.Expert;
			character.Medicine.Proficiency = Proficiency.Trained;
			character.Diplomacy.Proficiency = Proficiency.Trained;
			character.Arcana.Proficiency = Proficiency.Trained;

			return character;
		}
	}
}
