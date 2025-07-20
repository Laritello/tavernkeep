using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Pathfinder;

namespace Tavernkeep.Domain.Extensions;

public static class CharacterExtensions
{
	public static int GetProficiencyBonus(this Proficiency proficiency, Character owner)
	{
		return (int)proficiency + (proficiency > Proficiency.Untrained ? owner.Level : 0);
	}
}
