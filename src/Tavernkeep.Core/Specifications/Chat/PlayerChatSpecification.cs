using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Specifications.Chat
{
    /// <summary>
    /// Specification implementation that provides message visibility based on <see cref="UserRole.Player"/> permissions.
    /// </summary>
    /// <param name="initiatorId"><see cref="Guid"/> of the request initiator.</param>
    public class PlayerChatSpecification(Guid initiatorId) 
        : Specification<Message>(x => x.RecipientId == null || x.RecipientId == initiatorId || x.SenderId == initiatorId)
    {
    }
}
