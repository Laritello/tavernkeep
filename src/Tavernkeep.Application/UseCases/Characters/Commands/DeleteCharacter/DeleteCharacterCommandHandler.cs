using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Commands.DeleteCharacter
{
	public class DeleteCharacterCommandHandler(IUserRepository userRepository, ICharacterRepository characterRepository) : IRequestHandler<DeleteCharacterCommand>
	{
		public async Task Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.FindAsync(request.CharacterId)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			characterRepository.Remove(character);
			await characterRepository.CommitAsync(cancellationToken);
		}
	}
}
