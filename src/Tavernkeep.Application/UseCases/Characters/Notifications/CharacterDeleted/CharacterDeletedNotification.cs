using MediatR;
using Tavernkeep.Domain.Entities.Pathfinder;
using Tavernkeep.Domain.Notifications;

namespace Tavernkeep.Application.UseCases.Characters.Notifications.CharacterDeleted;

public class CharacterDeletedNotification(Character character) : INotification, ICharacterNotification
{
	public Character Character { get; set; } = character;
}
