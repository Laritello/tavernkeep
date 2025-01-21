using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Contracts.Character.Dtos
{
	public class CharacterDto
	{
		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }
		public string Name { get; set; } = default!;

		public AncestryDto Ancestry { get; set; } = default!;
		public ClassDto Class { get; set; } = default!;

		public int HeroPoints { get; set; } = 1;

		public int Level { get; set; } = 1;
		public Health Health { get; set; } = default!;
		public ArmorDto Armor { get; set; } = default!;
		public required ICollection<AbilityDto> Abilities { get; set; }
		public required ICollection<SkillDto> Skills { get; set; }
		public required ICollection<SavingThrowDto> SavingThrows { get; set; }
		public SkillShortDto Perception { get; set; } = default!;
		public Dictionary<SpeedType, Speed> Speeds { get; set; } = default!;
		public List<Condition> Conditions { get; set; } = default!;
	}
}
