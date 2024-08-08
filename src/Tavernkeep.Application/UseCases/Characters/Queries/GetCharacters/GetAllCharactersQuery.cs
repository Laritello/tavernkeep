using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Queries.GetCharacters
{
	public class GetAllCharactersQuery : IRequest<Dictionary<Guid, Character>> { }
}
