using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Application.UseCases.Characters.Commands.ModifyHealth
{
	public class ModifyHealthCommand(Guid initiatorId, Guid characterId, int change) : IRequest<Health>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public int Change { get; set; } = change;
	}
}
