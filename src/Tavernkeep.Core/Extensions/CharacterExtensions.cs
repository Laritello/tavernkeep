using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Extensions
{
	public static class CharacterExtensions
	{
		public static Ability GetSkillAbility(this Character character, SkillType type)
		{
			return type switch
			{
				SkillType.Acrobatics => character.Dexterity,
				SkillType.Arcana => character.Intelligence,
				SkillType.Athletics => character.Strength,
				SkillType.Crafting => character.Intelligence,
				SkillType.Deception => character.Charisma,
				SkillType.Diplomacy => character.Charisma,
				SkillType.Intimidation => character.Charisma,
				SkillType.Medicine => character.Wisdom,
				SkillType.Nature => character.Wisdom,
				SkillType.Occultism => character.Intelligence,
				SkillType.Performance => character.Charisma,
				SkillType.Religion => character.Wisdom,
				SkillType.Society => character.Intelligence,
				SkillType.Stealth => character.Dexterity,
				SkillType.Survival => character.Wisdom,
				SkillType.Thievery => character.Dexterity,
				_ => throw new NotImplementedException()
			};
		}

		public static int GetProficiencyBonus(this Proficiency proficiency, Character owner)
		{
			return (int)proficiency + (proficiency > Proficiency.Untrained ? owner.Level : 0);
		}

		public static ModifierTarget ToTarget(this SkillType type) => (ModifierTarget)(int)type;
	}
}
