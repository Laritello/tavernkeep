using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Entities.Rolls;

namespace Tavernkeep.Core.Contracts.Chat.Dtos
{
    public class RollMessageDto : MessageDto
    {
        public RollType RollType { get; set; }
        public string Expression { get; set; } = default!;
        public RollResult Result { get; set; } = default!;
    }
}
