using AutoMapper;
using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.Actions.Characters.Queries.GetCharacter
{
    public class GetCharacterQueryHandler(ICharacterRepository characterRepository, IMapper mapper) : IRequestHandler<GetCharacterQuery, CharacterDto>
    {
        public async Task<CharacterDto> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
        {
            var character = await characterRepository.FindAsync(request.Id)
                ?? throw new BusinessLogicException("No character with provided ID found.");

            return mapper.Map<CharacterDto>(character);
        }
    }
}
