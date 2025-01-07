namespace Tavernkeep.Core.Contracts.Conditions.Dtos
{
	/// <summary>
	/// Condition DTO used for transfering information about applied condition.
	/// </summary>
	public record ConditionShortDto
	{
		public string Name { get; set; } = default!;
		public int Level { get; set; }
	}
}
