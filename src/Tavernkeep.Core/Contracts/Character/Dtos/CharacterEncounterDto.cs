namespace Tavernkeep.Core.Contracts.Character.Dtos
{
	public class CharacterEncounterDto
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public HealthDto Health { get; set; } = default!;
		public required ICollection<SavingThrowDto> SavingThrows { get; set; }
		public SkillShortDto Perception { get; set; } = default!;
	}
}
