using MediatR;
using Tavernkeep.Core.Contracts.Conditions.Dtos;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditConditions
{
	public class EditConditionsCommand(Guid initiatorId, Guid characterId, List<ConditionEditDto> conditions) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public List<ConditionEditDto> Conditions { get; set; } = conditions;
	}
}
