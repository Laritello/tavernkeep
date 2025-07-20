namespace Tavernkeep.Domain.Contracts.Conditions.Dtos
{
	/// <summary>
	/// Condition DTO used for transfering information about applied condition.
	/// </summary>
	public record ConditionEditDto
	{
		public string Name { get; set; } = default!;
		public int? Level { get; set; }
	}
}
