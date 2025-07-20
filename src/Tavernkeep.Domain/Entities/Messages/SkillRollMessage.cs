using Tavernkeep.Domain.Entities.Snapshots;

namespace Tavernkeep.Domain.Entities.Messages;

public class SkillRollMessage : RollMessage
{
	#region Constructors

	public SkillRollMessage() { }

	#endregion

	#region Properties

	public SkillSnapshot Skill { get; set; } = default!;

	#endregion
}
