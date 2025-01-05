using MediatR;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Messages;

namespace Tavernkeep.Application.UseCases.Roll.Commands.RollSavingThrow
{
	public class RollSavingThrowCommand(Guid initiatorId, Guid characterId, SavingThrowType savingThrowType, RollType rollType) : IRequest<SavingThrowRollMessage>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public SavingThrowType SavingThrowType { get; set; } = savingThrowType;
		public RollType RollType { get; set; } = rollType;
	}
}
