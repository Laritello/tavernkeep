using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Rolls;

namespace Tavernkeep.Domain.Contracts.Chat.Dtos
{
	public class RollMessageDto : MessageDto
	{
		public RollType RollType { get; set; }
		public string Expression { get; set; } = default!;
		public RollResult Result { get; set; } = default!;
	}
}
