using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Characters.Queries.GetCharacter
{
    public class GetCharacterQuery : IRequest<Character>
    {
        public Guid Id { get; set; }

        public GetCharacterQuery(Guid id)
        {
            Id = id;
        }
    }
}
