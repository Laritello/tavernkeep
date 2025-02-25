namespace Tavernkeep.Core.Contracts.Conditions.Dtos
{
	public record ConditionDto
	{
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public bool HasLevels { get; set; }
		public int Level { get; set; }

		public List<ConditionDto> Related { get; set; } = [];
	}
}
