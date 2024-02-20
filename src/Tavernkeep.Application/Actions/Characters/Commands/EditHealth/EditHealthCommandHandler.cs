using MediatR;
using Tavernkeep.Core.Contracts.Character;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Characters.Commands.EditHealth
{
    public class EditHealthCommandHandler(IUserRepository userRepository, ICharacterRepository characterRepository) : IRequestHandler<EditHealthCommand, Health>
    {
        public async Task<Health> Handle(EditHealthCommand request, CancellationToken cancellationToken)
        {
            var initiator = await userRepository.FindAsync(request.InitiatorId)
                ?? throw new BusinessLogicException("User with specified ID doesn't exist.");

            var character = await characterRepository.GetFullCharacterAsync(request.CharacterId)
                ?? throw new BusinessLogicException("Character with specified ID doesn't exist.");

            if (character.Owner.Id != request.InitiatorId && initiator.Role != UserRole.Master)
                throw new InsufficientPermissionException("You do not have the necessary permissions to perform this operation.");

            // Health rules:
            // 1 - Max and temporary can't be bellow zero
            // 2 - Current must be in range [0; Max]
            character.Health.Max = Math.Max(0, request.Max);
            character.Health.Current = Math.Clamp(request.Current, 0, character.Health.Max);
            character.Health.Temporary = Math.Max(0, request.Temporary);

            characterRepository.Save(character);
            await characterRepository.CommitAsync(cancellationToken);

            return character.Health;
        }
    }
}
