using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommandHandler(
        IUserRepository userRepository,
        ICharacterService characterService
        ) : IRequestHandler<CreateCharacterCommand, Character>
    {
        public async Task<Character> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(request.OwnerId)
                ?? throw new BusinessLogicException("Owner with specified ID doesn't exist.");

            return await characterService.CreateCharacterAsync(user, request.Name, cancellationToken);
        }
    }
}
