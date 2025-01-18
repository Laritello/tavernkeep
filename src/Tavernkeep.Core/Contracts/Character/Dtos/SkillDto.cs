using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Dtos
{
	public class SkillDto
	{
		public required string Name { get; set; }
		public required Proficiency Proficiency { get; set; }
		public required int Bonus { get; set; }
	}
}
