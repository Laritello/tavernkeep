using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditSavingThrow
{
	public class EditSavingThrowCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService
		) : IRequestHandler<EditSavingThrowCommand, SavingThrow>
	{
		public async Task<SavingThrow> Handle(EditSavingThrowCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			var savingThrow = character.GetSavingThrow(request.Type);
			savingThrow.Proficiency = request.Proficiency;

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(character, cancellationToken);

			return savingThrow;
		}
	}
}
