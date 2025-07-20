using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Character.Dtos;

public class SkillDto
{
	public required string Name { get; set; }
	public required SkillType Type { get; set; }
	public required Proficiency Proficiency { get; set; }
	public bool Pinned { get; set; }
	public int Bonus { get; set; }
}
