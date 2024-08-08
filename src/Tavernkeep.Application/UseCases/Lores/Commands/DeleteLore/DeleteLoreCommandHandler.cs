using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Lores.Commands.DeleteLore
{
	public class DeleteLoreCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository
		) : IRequestHandler<DeleteLoreCommand>
	{
		public async Task Handle(DeleteLoreCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			var lore = character.Lores.FirstOrDefault(x => x.Topic == request.Topic)
				?? throw new BusinessLogicException("Character does not have lore skill with this topic.");

			character.Lores.Remove(lore);

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
		}
	}
}
