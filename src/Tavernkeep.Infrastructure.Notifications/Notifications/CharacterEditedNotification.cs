using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Infrastructure.Notifications.Notifications
{
	public class CharacterEditedNotification(Character character)
	{
		public Character Character { get; set; } = character;
	}
}
