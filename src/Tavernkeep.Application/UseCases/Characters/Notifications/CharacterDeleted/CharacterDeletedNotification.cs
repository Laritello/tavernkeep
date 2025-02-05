using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Notifications.CharacterDeleted
{
	public class CharacterDeletedNotification(Character character) : INotification, ICharacterNotification
	{
		public Character Character { get; set; } = character;
	}
}
