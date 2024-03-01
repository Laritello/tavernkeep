using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Characters.Queries.GetCharacter
{
    public class GetCharacterQuery : IRequest<CharacterDto>
    {
        public Guid Id { get; set; }

        public GetCharacterQuery(Guid id)
        {
            Id = id;
        }
    }
}
