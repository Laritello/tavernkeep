namespace Tavernkeep.Core.Contracts.Character.Dtos
{
	/// <summary>
	/// <see cref="CharacterTemplateDto"/> is a data transfer object used to provide default values for character creation and to capture user-customized character attributes for submission to the server.
	/// </summary>
	public class CharacterTemplateDto
	{
		public string Name { get; set; } = default!;
		public int Level { get; set; }

		public AncestryDto Ancestry { get; set; } = default!;
		public ClassDto Class { get; set; } = default!;
		public required ICollection<AbilityDto> Abilities { get; set; }
		public required ICollection<SkillDto> Skills { get; set; }
		public required ICollection<SavingThrowDto> SavingThrows { get; set; }
		public SkillShortDto Perception { get; set; } = default!;
	}
}
