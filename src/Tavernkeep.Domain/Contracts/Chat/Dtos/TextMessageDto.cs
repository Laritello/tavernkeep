using Tavernkeep.Domain.Contracts.Users.Dtos;

namespace Tavernkeep.Domain.Contracts.Chat.Dtos;

public class TextMessageDto : MessageDto
{
	public UserDto Recipient { get; set; } = default!;
	public string Text { get; set; } = default!;
	public bool IsPrivate { get; set; }
}
