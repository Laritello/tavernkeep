using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Creatures.Queries.GetCreature
{
	public class GetCreatureQueryHandler(ICreatureLibraryRepository creatureRepository) : IRequestHandler<GetCreatureQuery, Creature>
	{
		public async Task<Creature> Handle(GetCreatureQuery request, CancellationToken cancellationToken)
		{
			return await creatureRepository.GetCreatureAsync(request.Id, cancellationToken);
		}
	}
}
