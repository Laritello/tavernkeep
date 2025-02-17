using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Creatures.Queries
{
	public class GetCreaturesQuery : IRequest<ICollection<Creature>> { }
}
