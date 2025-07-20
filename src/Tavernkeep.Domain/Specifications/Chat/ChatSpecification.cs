using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities;
using Tavernkeep.Domain.Entities.Messages;

namespace Tavernkeep.Domain.Specifications.Chat;

public class ChatSpecification(User initiator) : Specification<Message>
	(x => x.SenderId == initiator.Id ||
	(x is TextMessage && ((TextMessage)x).RecipientId == null || (((TextMessage)x).RecipientId == initiator.Id)) ||
	(x is RollMessage && ((RollMessage)x).RollType == RollType.Public)
	)
{
}
