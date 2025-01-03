using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrow
{
	public class EditSavingThrowsCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService
		) : IRequestHandler<EditSavingThrowsCommand>
	{
		public async Task Handle(EditSavingThrowsCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			foreach (var key in request.Proficiencies.Keys)
			{
				var savingThrow = character.GetSavingThrow(key);
				savingThrow.Proficiency = request.Proficiencies[key];
			}

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(character, cancellationToken);
		}
	}
}
