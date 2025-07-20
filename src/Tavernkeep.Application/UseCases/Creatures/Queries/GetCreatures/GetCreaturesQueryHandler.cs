using MediatR;
using Tavernkeep.Domain.Entities.Library.Creatures;
using Tavernkeep.Domain.Repositories;

namespace Tavernkeep.Application.UseCases.Creatures.Queries.GetCreatures;

public class GetCreaturesQueryHandler(ICreatureLibraryRepository creatureRepository) : IRequestHandler<GetCreaturesQuery, ICollection<Creature>>
{
	public async Task<ICollection<Creature>> Handle(GetCreaturesQuery request, CancellationToken cancellationToken)
	{
		return await creatureRepository.GetAllCreaturesAsync(cancellationToken);
	}
}
