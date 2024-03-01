using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Chat.Dtos
{
    public class RollMessageDto : MessageDto
    {
        public int Result { get; set; }
        public RollType RollType { get; set; }
    }
}
