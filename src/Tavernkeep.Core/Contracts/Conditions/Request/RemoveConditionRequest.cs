namespace Tavernkeep.Core.Contracts.Conditions.Request
{
	public class RemoveConditionRequest
	{
		public Guid CharacterId { get; set; }
		public string ConditionName { get; set; } = default!;
	}
}
