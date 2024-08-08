using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;

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
