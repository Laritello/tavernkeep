using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Characters.Queries.GetCharacter
{
    public class GetCharacterQueryHandler(ICharacterRepository characterRepository) : IRequestHandler<GetCharacterQuery, Character>
    {
        public async Task<Character> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.FindAsync(request.Id)
                ?? throw new BusinessLogicException("No character with provided ID found.");

            return character;
        }
    }
}
