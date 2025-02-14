using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Rolls.Commands.RollCustomDice
{
	public class RollCustomDiceCommand(Guid initiatorId, RollType rollType, string expression) : IRequest<RollMessage>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public RollType RollType { get; set; } = rollType;
		public string Expression { get; set; } = expression;
	}
}
