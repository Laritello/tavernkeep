using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Lores.Commands.EditLore
{
	public class EditLoreCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository
		) : IRequestHandler<EditLoreCommand, Lore>
	{
		public async Task<Lore> Handle(EditLoreCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			var lore = character.Lores.FirstOrDefault(x => x.Topic == request.Topic)
				?? throw new BusinessLogicException("Character does not have lore skill with this topic.");

			lore.Proficiency = request.Proficiency;

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);

			return lore;
		}
	}
}
