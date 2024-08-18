using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Infrastructure.Notifications.Notifications
{
	public class CharacterEditedNotification(CharacterDto character)
	{
		public CharacterDto Character { get; set; } = character;
	}
}
