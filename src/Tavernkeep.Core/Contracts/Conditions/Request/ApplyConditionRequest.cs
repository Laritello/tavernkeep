namespace Tavernkeep.Core.Contracts.Conditions.Request
{
	public class ApplyConditionRequest
	{
		public Guid CharacterId { get; set; }
		public string ConditionName { get; set; } = default!;
		public int? ConditionLevel { get; set; }
	}
}
