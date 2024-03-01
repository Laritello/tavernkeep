using AutoMapper;
using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Characters.Queries.GetCharacters
{
    public class GetAllCharactersQueryHandler(
        ICharacterRepository characterRepository, 
        IMapper mapper
        ) : IRequestHandler<GetAllCharactersQuery, List<CharacterDto>>
    {
        public async Task<List<CharacterDto>> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken)
        {
            var characters = await characterRepository.GetAllCharactersAsync(cancellationToken);
            return mapper.Map<List<CharacterDto>>(characters);
        }
    }
}
