using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Creatures.Queries.GetCreatures
{
	public class GetCreaturesQuery : IRequest<ICollection<Creature>> { }
}
