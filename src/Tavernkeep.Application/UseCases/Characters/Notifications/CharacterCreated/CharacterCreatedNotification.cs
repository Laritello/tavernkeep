using MediatR;
using Tavernkeep.Domain.Entities.Pathfinder;
using Tavernkeep.Domain.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Notifications.CharacterCreated
{
	public class CharacterCreatedNotification(Character character) : INotification, ICharacterNotification
	{
		public Character Character { get; set; } = character;
	}
}
