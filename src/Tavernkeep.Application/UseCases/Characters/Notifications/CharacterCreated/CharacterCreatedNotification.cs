using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Notifications.CharacterCreated
{
	public class CharacterCreatedNotification(Character character) : INotification, ICharacterNotification
	{
		public Character Character { get; set; } = character;
	}
}
