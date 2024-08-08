using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Conditions.Commands.ApplyCondition
{
	public class ApplyConditionCommand(Guid initiatorId, Guid characterId, string conditionName, int? conditionLevel = 1) : IRequest<Character>
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;
		public string ConditionName { get; set; } = conditionName;
		public int? ConditionLevel { get; set; } = conditionLevel;
	}
}
