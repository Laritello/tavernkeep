using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Users.Commands.SetActiveCharacter
{
    public class SetActiveCharacterCommandHandler(
        IUserRepository userRepository, 
        ICharacterRepository characterRepository
        ) : IRequestHandler<SetActiveCharacterCommand, User>
    {
        public async Task<User> Handle(SetActiveCharacterCommand request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId)
                ?? throw new BusinessLogicException("Initiator with specified ID doesn't exist.");

            var user = await userRepository.FindAsync(request.UserId)
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            var character = await characterRepository.GetFullCharacterAsync(request.CharacterId, cancellationToken)
                ?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

            if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
                throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

            if (character.Owner.Id != request.UserId)
                throw new BusinessLogicException("Character cannot be set as active character for the user that is not his owner.");

            user.ActiveCharacter = character;

            userRepository.Save(user);
            await userRepository.CommitAsync(cancellationToken);

            return user;
        }
    }
}
