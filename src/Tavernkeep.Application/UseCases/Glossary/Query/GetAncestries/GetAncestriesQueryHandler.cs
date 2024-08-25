using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Ancestries;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Glossary.Query.GetAncestries
{
	public class GetAncestriesQueryHandler(IAncestryMetadataRepository ancestryRepository) : IRequestHandler<GetAncestriesQuery, List<AncestryMetadata>>
	{
		public async Task<List<AncestryMetadata>> Handle(GetAncestriesQuery request, CancellationToken cancellationToken)
		{
			return await ancestryRepository.GetAllAncestriesAsync(cancellationToken);
		}
	}
}
