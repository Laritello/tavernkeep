using Tavernkeep.Core.Entities.Snapshots;

namespace Tavernkeep.Core.Contracts.Chat.Dtos
{
	public class SkillRollMessageDto : RollMessageDto
	{
		public SkillSnapshot Skill { get; set; } = default!;
	}
}
