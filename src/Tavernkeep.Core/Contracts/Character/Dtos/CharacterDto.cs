using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Core.Contracts.Character.Dtos
{
	public class CharacterDto
	{
		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }
		public string Name { get; set; } = default!;
		public Health Health { get; set; } = default!;
		public ArmorClass Armor { get; set; } = default!;
		public Dictionary<AbilityType, Ability> Abilities { get; set; } = default!;
		public Dictionary<SkillType, Skill> Skills { get; set; } = default!;
		public Dictionary<SavingThrowType, SavingThrow> SavingThrows { get; set; } = default!;
		public List<Lore> Lores { get; set; } = default!;
		public List<Condition> Conditions { get; set; } = default!;
	}
}
