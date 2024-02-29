using MediatR;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.Actions.Characters.Queries.GetCharacters
{
    public class GetAllCharactersQuery : IRequest<List<CharacterDto>> { }
}
