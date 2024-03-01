using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Users.Dtos;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Core.Contracts.Chat.Dtos
{
    [JsonDerivedType(typeof(TextMessageDto), typeDiscriminator: nameof(TextMessage))]
    [JsonDerivedType(typeof(RollMessageDto), typeDiscriminator: nameof(RollMessage))]
    public abstract class MessageDto
    {
        public Guid Id { get; set; }
        public UserDto Sender { get; set; } = default!;
        public DateTime Created { get; set; }
    }
}
