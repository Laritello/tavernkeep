using MediatR;
using Tavernkeep.Core.Contracts.Conditions.Dtos;

namespace Tavernkeep.Application.UseCases.Conditions.Commands.EditConditions
{
	public class EditConditionsCommand(Guid initiatorId, Guid characterId, List<ConditionShortDto> conditions) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public List<ConditionShortDto> Conditions { get; set; } = conditions;
	}
}
