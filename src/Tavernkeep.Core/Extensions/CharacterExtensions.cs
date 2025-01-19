using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Extensions
{
	public static class CharacterExtensions
	{
		public static int GetProficiencyBonus(this Proficiency proficiency, Character owner)
		{
			return (int)proficiency + (proficiency > Proficiency.Untrained ? owner.Level : 0);
		}
	}
}
