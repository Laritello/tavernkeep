using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Dtos
{
	public class SkillEditDto
	{
		public Proficiency? Proficiency { get; set; }
		public bool? Pinned { get; set; }
	}
}
