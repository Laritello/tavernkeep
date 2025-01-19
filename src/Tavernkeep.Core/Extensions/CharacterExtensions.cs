using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Extensions
{
	public static class CharacterExtensions
	{
		public static int GetProficiencyBonus(this Proficiency proficiency, Character owner)
		{
			return (int)proficiency + (proficiency > Proficiency.Untrained ? owner.Level : 0);
		}

		public static ModifierTarget ToTarget(this SkillType type) => (ModifierTarget)((int)type + (int)ModifierTarget.Acrobatics);

		public static ModifierTarget ToTarget(this SavingThrowType type) => (ModifierTarget)((int)type + (int)ModifierTarget.Fortitude);

		public static ModifierTarget ToTarget(this SpeedType type) => (ModifierTarget)((int)type + (int)ModifierTarget.WalkSpeed);
	}
}
