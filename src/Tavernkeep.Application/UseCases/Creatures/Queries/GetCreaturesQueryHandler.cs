using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Creatures.Queries
{
	public class GetCreaturesQueryHandler(ICreatureLibraryRepository creatureRepository) : IRequestHandler<GetCreaturesQuery, ICollection<Creature>>
	{
		public async Task<ICollection<Creature>> Handle(GetCreaturesQuery request, CancellationToken cancellationToken)
		{
			return await creatureRepository.GetAllCreaturesAsync(cancellationToken);
		}
	}
}
