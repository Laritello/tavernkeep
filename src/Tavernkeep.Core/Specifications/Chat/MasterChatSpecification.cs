using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Core.Specifications.Chat
{
    /// <summary>
    /// Specification implementation that provides message visibility based on <see cref="UserRole.Master"/> permissions.
    /// </summary>
    /// <param name="initiatorId"><see cref="Guid"/> of the request initiator.</param>
    public class MasterChatSpecification(Guid initiatorId)
        : Specification<Message>(x => x.RecipientId == null || x.Type == MessageType.Roll || x.RecipientId == initiatorId || x.SenderId == initiatorId)
    {
    }
}
