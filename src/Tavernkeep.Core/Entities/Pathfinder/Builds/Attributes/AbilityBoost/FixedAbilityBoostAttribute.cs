using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Values;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityBoost
{
	public class FixedAbilityBoostAttribute : AbilityBoostAttribute
	{
		public override BuildAttribute Convert(BuildSnapshot snapshot, ConversionParameters? parameters = null) => Convert(snapshot.Values, parameters);

		public override BuildAttribute Convert(List<BuildValue> values, ConversionParameters? parameters = null)
		{
			return new FixedAbilityBoostAttribute()
			{
				Id = Id,
				Level = Level,
				Selected = Selected,
			};
		}

		public override BuildValue Snapshot()
		{
			return new AbilityBoostValue()
			{
				Id = Id,
				Selected = Selected,
			};
		}
	}
}
