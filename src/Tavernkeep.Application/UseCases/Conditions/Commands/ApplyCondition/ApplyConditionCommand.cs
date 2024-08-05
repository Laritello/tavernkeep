using MediatR;
using Tavernkeep.Core.Entities;

namespace Tavernkeep.Application.UseCases.Conditions.Commands.ApplyCondition
{
	public class ApplyConditionCommand : IRequest<Character>
	{
		public Guid InitiatorId { get; set; }
		public Guid CharacterId { get; set; }
		public string ConditionName { get; set; }
		public int? ConditionLevel { get; set; }

		public ApplyConditionCommand(Guid initiatorId, Guid characterId, string conditionName, int? conditionLevel)
		{
			InitiatorId = initiatorId;
			CharacterId = characterId;
			ConditionName = conditionName;
			ConditionLevel = conditionLevel;
		}
	}
}
