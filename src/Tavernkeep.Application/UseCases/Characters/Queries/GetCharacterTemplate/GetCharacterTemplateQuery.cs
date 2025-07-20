using MediatR;
using Tavernkeep.Domain.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Characters.Queries.GetCharacterTemplate
{
	public class GetCharacterTemplateQuery : IRequest<Character>
	{
	}
}
