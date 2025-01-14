using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditHeroPoints
{
	public class EditHeroPointsCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository,
		INotificationService notificationService
		) : IRequestHandler<EditHeroPointsCommand>
	{
		public async Task Handle(EditHeroPointsCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			if (request.Amount < 0)
				throw new BusinessLogicException("Hero points amount cannot be less than zero.");

			if (request.Amount > 3)
				throw new BusinessLogicException("Hero points amoint cannot be more than three.");

			character.HeroPoints = request.Amount;

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			await notificationService.QueueCharacterNotificationAsync(character, cancellationToken);
		}
	}
}
