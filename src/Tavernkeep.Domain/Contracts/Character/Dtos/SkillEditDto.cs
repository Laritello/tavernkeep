using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Character.Dtos;

public class SkillEditDto
{
	public Proficiency? Proficiency { get; set; }
	public bool? Pinned { get; set; }
}
