using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Characters.Queries.GetCharacters
{
    public class GetAllCharactersQuery : IRequest<Dictionary<Guid, Character>> { }
}
