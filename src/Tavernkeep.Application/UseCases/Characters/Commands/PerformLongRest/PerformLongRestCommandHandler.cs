using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Commands.PerformLongRest
{
	public class PerformLongRestCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService
		) : IRequestHandler<PerformLongRestCommand>
	{
		public async Task Handle(PerformLongRestCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			/*
			 * The character regains Hit Points equal to their Constitution modifier (minimum 1) multiplied by their level. 
			 * The character loses the fatigued condition.
			 * The character reduces the severity of the doomed and drained conditions by 1.
			 * Most spellcasters need to rest before they regain their spells for the day.
			*/

			character.Health.Current += character.Constitution.Modifier * character.Level;
			character.Conditions.RemoveAll(x => x.Name == "Fatigued");

			// Using to list call, because we might want to delete condition for the collection
			// and this will lead to an error. Performance hit is negligible since rarely character has more than 2-3 conditions
			// at once.
			foreach (var condition in character.Conditions.Where(x => x.Name is "Doomed" or "Drained").ToList())
			{
				if (condition.Level == 1)
				{
					character.Conditions.Remove(condition);
				}
				else
				{
					condition.Level -= 1;
				}
			}

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(character, cancellationToken);
		}
	}
}
