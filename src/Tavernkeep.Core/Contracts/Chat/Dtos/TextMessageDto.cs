using Tavernkeep.Core.Contracts.Users.Dtos;

namespace Tavernkeep.Core.Contracts.Chat.Dtos
{
    public class TextMessageDto : MessageDto
    {
        public UserDto Recipient { get; set; } = default!;
        public string Text { get; set; } = default!;
        public bool IsPrivate { get; set; }
    }
}
