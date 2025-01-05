using Tavernkeep.Core.Entities.Snapshots;

namespace Tavernkeep.Core.Contracts.Chat.Dtos
{
	public class SavingThrowRollMessageDto : RollMessageDto
	{
		public SavingThrowSnapshot SavingThrow { get; set; } = default!;
	}
}
