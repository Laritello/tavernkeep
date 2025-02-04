using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Notifications.CharacterEdited
{
	public class CharacterEditedNotification(Character character) : INotification, ICharacterNotification
	{
		public Character Character { get; set; } = character;
	}
}
