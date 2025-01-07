using Tavernkeep.Core.Contracts.Conditions.Dtos;

namespace Tavernkeep.Core.Contracts.Conditions.Request
{
	public class EditConditionsRequest
	{
		public List<ConditionShortDto> Conditions { get; set; } = [];
	}
}
