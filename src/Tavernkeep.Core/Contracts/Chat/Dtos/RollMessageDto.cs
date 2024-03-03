using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Rolls;

namespace Tavernkeep.Core.Contracts.Chat.Dtos
{
    public class RollMessageDto : MessageDto
    {
        public RollResult Result { get; set; } = default!;
        public RollType RollType { get; set; }
    }
}
