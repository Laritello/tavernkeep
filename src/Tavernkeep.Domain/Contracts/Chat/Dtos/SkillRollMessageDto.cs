using Tavernkeep.Domain.Entities.Snapshots;

namespace Tavernkeep.Domain.Contracts.Chat.Dtos;

public class SkillRollMessageDto : RollMessageDto
{
	public SkillSnapshot Skill { get; set; } = default!;
}
