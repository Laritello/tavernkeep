using MediatR;
using Tavernkeep.Domain.Entities.Library.Creatures;

namespace Tavernkeep.Application.UseCases.Creatures.Queries.GetCreature
{

	public class GetCreatureQuery(Guid id) : IRequest<Creature>
	{
		public Guid Id { get; set; } = id;

	}
}
