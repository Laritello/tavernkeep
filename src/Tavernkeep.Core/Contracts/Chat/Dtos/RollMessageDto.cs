using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Entities.Rolls;

namespace Tavernkeep.Core.Contracts.Chat.Dtos
{
    [JsonDerivedType(typeof(SkillRollMessageDto), typeDiscriminator: nameof(SkillRollMessage))]
    public class RollMessageDto : MessageDto
    {
        public RollType RollType { get; set; }
        public string Expression { get; set; } = default!;
        public RollResult Result { get; set; } = default!;
    }
}
