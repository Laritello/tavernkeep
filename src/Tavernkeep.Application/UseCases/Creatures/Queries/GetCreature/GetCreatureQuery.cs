using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Creatures.Queries.GetCreature
{

	public class GetCreatureQuery(Guid id) : IRequest<Creature>
	{
		public Guid Id { get; set; } = id;

	}
}
