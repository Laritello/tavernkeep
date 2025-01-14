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
		public string Class { get; set; } = default!;
		public string Ancestry { get; set; } = default!;

		public int HeroPoints { get; set; } = 1;

		public int Level { get; set; } = 1;
		public Health Health { get; set; } = default!;
		public ArmorDto Armor { get; set; } = default!;
		public Dictionary<AbilityType, Ability> Abilities { get; set; } = default!;
		public Dictionary<SkillType, Skill> Skills { get; set; } = default!;
		public Perception Perception { get; set; } = default!;
		public Dictionary<SavingThrowType, SavingThrow> SavingThrows { get; set; } = default!;
		public Dictionary<SpeedType, Speed> Speeds { get; set; } = default!;
		public List<Lore> Lores { get; set; } = default!;
		public List<Condition> Conditions { get; set; } = default!;
	}
}
