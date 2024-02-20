using MediatR;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Characters.Commands.DeleteCharacter
{
    public class DeleteCharacterCommandHandler(ICharacterRepository characterRepository) : IRequestHandler<DeleteCharacterCommand>
    {
        public async Task Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.FindAsync(request.CharacterId)
                ?? throw new BusinessLogicException("Character not found");

            characterRepository.Remove(character);
            await characterRepository.CommitAsync(cancellationToken);
        }
    }
}
