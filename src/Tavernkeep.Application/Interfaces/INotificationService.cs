using Tavernkeep.Core.Entities.Messages;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.Interfaces
{
	public interface INotificationService
	{
		ValueTask QueueMessageAsync(Message message, CancellationToken cancellationToken = default);
		ValueTask QueueDeleteMessageAsync(Message message, CancellationToken cancellationToken = default);
		ValueTask QueueCharacterNotificationAsync(Character character, CancellationToken cancellationToken = default);
	}
}
