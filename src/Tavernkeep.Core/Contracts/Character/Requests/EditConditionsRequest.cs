using Tavernkeep.Core.Contracts.Conditions.Dtos;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
	public class EditConditionsRequest
	{
		public List<ConditionEditDto> Conditions { get; set; } = [];
	}
}
