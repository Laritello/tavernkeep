using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

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

		public static Ability GetSavingThrowAbility(this Character character, SavingThrowType type)
		{
			return type switch
			{
				SavingThrowType.Fortitude => character.Constitution,
				SavingThrowType.Reflex => character.Dexterity,
				SavingThrowType.Will => character.Wisdom,
				_ => throw new NotImplementedException()
			};
		}

		public static int GetProficiencyBonus(this Proficiency proficiency, Character owner)
		{
			return (int)proficiency + (proficiency > Proficiency.Untrained ? owner.Level : 0);
		}

		public static ModifierTarget ToTarget(this SkillType type) => (ModifierTarget)((int)type + (int)ModifierTarget.Acrobatics);

		public static ModifierTarget ToTarget(this SavingThrowType type) => (ModifierTarget)((int)type + (int)ModifierTarget.Fortitude);

		public static ModifierTarget ToTarget(this SpeedType type) => (ModifierTarget)((int)type + (int)ModifierTarget.WalkSpeed);
	}
}
