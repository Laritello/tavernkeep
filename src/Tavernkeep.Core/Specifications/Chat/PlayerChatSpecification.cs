using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Core.Specifications.Chat
{
    /// <summary>
    /// Specification implementation that provides message visibility based on <see cref="UserRole.Player"/> permissions.
    /// </summary>
    /// <param name="initiator"><see cref="User"/> that initiated the specification.</param>
    public class PlayerChatSpecification(User initiator) : Specification<Message>(x => x.CheckVisbility(initiator))
    {
        
    }
}
