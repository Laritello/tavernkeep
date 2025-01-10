using MediatR;

namespace Tavernkeep.Application.UseCases.Characters.Commands.PerformLongRest
{
	public class PerformLongRestCommand(Guid initiatorId, Guid characterId) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
	}
}
