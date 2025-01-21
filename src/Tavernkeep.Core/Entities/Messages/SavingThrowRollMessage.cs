using Tavernkeep.Core.Entities.Snapshots;

namespace Tavernkeep.Core.Entities.Messages
{
	public class SavingThrowRollMessage : RollMessage
	{
		#region Constructors

		public SavingThrowRollMessage() { }

		#endregion

		#region Properties

		public SkillSnapshot SavingThrow { get; set; } = default!;

		#endregion
	}
}
