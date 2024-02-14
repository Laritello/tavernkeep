using MediatR;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Users.Commands.SelectActiveCharacter
{
    public class SelectActiveCharacterCommandHandler(IUserRepository userRepository, ICharacterRepository characterRepository) : IRequestHandler<SelectActiveCharacterCommand>
    {
        public async Task Handle(SelectActiveCharacterCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(request.UserId)
                ?? throw new BusinessLogicException("Owner with specified ID doesn't exist.");

            var character = await characterRepository.GetFullCharacter(request.CharacterId, cancellationToken)
                ?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

            if (character.Owner.Id != user.Id)
                throw new BusinessLogicException("Can't set character as active, because it doesn't belong to the provided user.");

            user.ActiveCharacter = character;
            userRepository.Save(user);
            await userRepository.CommitAsync(cancellationToken);
        }
    }
}
