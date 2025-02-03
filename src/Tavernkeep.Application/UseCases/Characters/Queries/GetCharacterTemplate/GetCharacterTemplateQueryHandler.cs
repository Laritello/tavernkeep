using MediatR;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Queries.GetCharacterTemplate
{
	public class GetCharacterTemplateQueryHandler(ICharacterService characterService) : IRequestHandler<GetCharacterTemplateQuery, Character>
	{
		public Task<Character> Handle(GetCharacterTemplateQuery request, CancellationToken cancellationToken)
		{
			return characterService.CreateCharacterTemplateAsync(cancellationToken);
		}
	}
}
