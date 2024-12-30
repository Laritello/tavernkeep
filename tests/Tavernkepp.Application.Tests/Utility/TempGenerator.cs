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
			};

			return character;
		}
	}
}
