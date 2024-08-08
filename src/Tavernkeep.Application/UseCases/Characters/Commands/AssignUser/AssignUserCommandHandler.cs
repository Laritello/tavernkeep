using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Commands.AssignUser
{
	public class AssignUserCommandHandler(
		IUserRepository userRepository,
		ICharacterRepository characterRepository
		) : IRequestHandler<AssignUserCommand, Character>
	{
		public async Task<Character> Handle(AssignUserCommand request, CancellationToken cancellationToken)
		{
			var character = await characterRepository.FindAsync(request.CharacterId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

			var user = await userRepository.FindAsync(request.UserId, cancellationToken: cancellationToken)
				?? throw new BusinessLogicException("User with specified ID doesn't exist.");

			character.Owner = user;
			characterRepository.Save(character);
			await characterRepository.CommitAsync(cancellationToken);
			// TODO: SignalR notification

			return character;
		}
	}
}
