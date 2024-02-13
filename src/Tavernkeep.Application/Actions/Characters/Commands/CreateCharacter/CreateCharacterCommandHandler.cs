using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Characters.Commands.CreateCharacter
{
    public class CreateCharacterCommandHandler(IUserRepository userRepository, ICharacterRepository characterRepository) : IRequestHandler<CreateCharacterCommand, Character>
    {
        public async Task<Character> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.FindAsync(request.OwnerId) 
                ?? throw new BusinessLogicException("Owner with specified ID doesn't exist.");

            Character character = new()
            {
                Owner = user,
                Name = request.Name,
            };

            characterRepository.Save(character);
            await characterRepository.CommitAsync(cancellationToken);

            return character;
        }
    }
}
