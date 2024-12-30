using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Queries.GetCharacter
{
	public class GetCharacterQueryHandler(ICharacterService characterService) : IRequestHandler<GetCharacterQuery, Character>
	{
		public async Task<Character> Handle(GetCharacterQuery request, CancellationToken cancellationToken)
		{
			return await characterService.GetCharacterAsync(request.Id, cancellationToken);
		}
	}
}
