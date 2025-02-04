using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Dtos
{
	public class SavingThrowDto
	{
		public required string Name { get; set; }
		public required Proficiency Proficiency { get; set; }
		public int Bonus { get; set; }
	}
}
