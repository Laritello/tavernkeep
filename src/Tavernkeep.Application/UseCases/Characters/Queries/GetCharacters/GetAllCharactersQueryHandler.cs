using MediatR;
using Tavernkeep.Core.Entities;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Characters.Queries.GetCharacters
{
	public class GetAllCharactersQueryHandler(
		ICharacterRepository characterRepository
		) : IRequestHandler<GetAllCharactersQuery, Dictionary<Guid, Character>>
	{
		public async Task<Dictionary<Guid, Character>> Handle(GetAllCharactersQuery request, CancellationToken cancellationToken)
		{
			var characters = await characterRepository.GetAllCharactersAsync(cancellationToken);
			return characters.ToDictionary(x => x.Id);
		}
	}
}
