using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Core.Specifications.Chat
{
	public class ChatSpecification(User initiator) : Specification<Message>
		(x => x.SenderId == initiator.Id ||
		(x is TextMessage && ((TextMessage)x).RecipientId == null || (((TextMessage)x).RecipientId == initiator.Id)) ||
		(x is RollMessage && ((RollMessage)x).RollType == RollType.Public)
		)
	{
	}
}
