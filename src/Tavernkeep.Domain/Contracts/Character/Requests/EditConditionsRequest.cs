using Tavernkeep.Domain.Contracts.Conditions.Dtos;

namespace Tavernkeep.Domain.Contracts.Character.Requests
{
	public class EditConditionsRequest
	{
		public List<ConditionEditDto> Conditions { get; set; } = [];
	}
}
