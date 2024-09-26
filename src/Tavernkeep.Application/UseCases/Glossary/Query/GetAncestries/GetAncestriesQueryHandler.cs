using MediatR;
using Tavernkeep.Core.Entities.Templates;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Glossary.Query.GetAncestries
{
	public class GetAncestriesQueryHandler(IAncestryTemplateRepository ancestryRepository) : IRequestHandler<GetAncestriesQuery, List<AncestryTemplate>>
	{
		public async Task<List<AncestryTemplate>> Handle(GetAncestriesQuery request, CancellationToken cancellationToken)
		{
			return await ancestryRepository.GetAllAncestriesAsync(cancellationToken);
		}
	}
}
