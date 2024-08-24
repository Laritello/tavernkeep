namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements
{
	public class KeyAbilityAdvancement : AbilityBoostAdvancement
	{
		public override string ToString() => $"KeyAbilityAdvancement [Level {Level}]: {(Selected.HasValue ? Selected : "Not Selected")}";
	}
}
