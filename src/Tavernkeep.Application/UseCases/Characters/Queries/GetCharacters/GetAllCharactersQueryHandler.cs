using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Characters.Queries.GetCharacters
{
    public class GetAllCharactersQueryHandler(
        ICharacterRepository characterRepository
        ) : IRequestHandler<GetAllCharactersQuery, List<Character>>
    {
        public async Task<List<Character>> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken)
        {
            return await characterRepository.GetAllCharactersAsync(cancellationToken);
        }
    }
}
