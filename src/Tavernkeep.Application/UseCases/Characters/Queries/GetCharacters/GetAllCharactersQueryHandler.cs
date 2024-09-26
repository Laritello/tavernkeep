using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Queries.GetCharacters
{
	public class GetAllCharactersQueryHandler(
		ICharacterService characterService
		) : IRequestHandler<GetAllCharactersQuery, Dictionary<Guid, Character>>
	{
		public async Task<Dictionary<Guid, Character>> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken)
		{
			var characters = await characterService.GetAllCharactersAsync(cancellationToken);
			return characters.ToDictionary(x => x.Id);
		}
	}
}
