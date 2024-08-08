using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Lores.Commands.CreateLore
{
	public class CreateLoreCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository
		) : IRequestHandler<CreateLoreCommand, Lore>
	{
		public async Task<Lore> Handle(CreateLoreCommand request, CancellationToken cancellationToken)
		{
			var initiator = await userRepository.FindAsync(request.InitiatorId)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
				throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

			if (character.Lores.Any(x => x.Topic == request.Topic))
				throw new BusinessLogicException("Character already has lore skill with this topic.");

			var lore = new Lore()
			{
				Owner = character,
				Topic = request.Topic,
				Proficiency = request.Proficiency,
			};

			character.Lores.Add(lore);

			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);

			return lore;
		}
	}
}
