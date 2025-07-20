namespace Tavernkeep.Domain.Contracts.Conditions.Dtos
{
	public record ConditionShortDto
	{
		public string Name { get; set; } = default!;
		public bool HasLevels { get; set; }
		public int? Level { get; set; }
	}
}
