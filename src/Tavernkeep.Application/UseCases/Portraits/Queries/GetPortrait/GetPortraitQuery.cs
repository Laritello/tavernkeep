using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Portraits.Queries.GetPortrait
{
	public class GetPortraitQuery(Guid characterId) : IRequest<Portrait>
	{
		public Guid CharacterId { get; set; } = characterId;
	}
}
