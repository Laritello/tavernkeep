using MediatR;

namespace Tavernkeep.Application.UseCases.Characters.Commands.ModifyHealth
{
	public class ModifyHealthCommand(Guid initiatorId, Guid characterId, int change) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public int Change { get; set; } = change;
	}
}
