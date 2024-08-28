using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Values;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityBoost
{
	public class VariableAbilityBoostAttribute : AbilityBoostAttribute
	{
		public List<AbilityType> Possible { get; set; } = [];
		public int Amount { get; set; }

		public override BuildAttribute Convert(BuildSnapshot snapshot, ConversionParameters? parameters = null) => Convert(snapshot.Values, parameters);

		public override BuildAttribute Convert(List<BuildValue> values, ConversionParameters? parameters = null)
		{
			return new VariableAbilityBoostAttribute()
			{
				Id = Id,
				Level = Level,
				Amount = Amount,
				Selected = values.FirstOrDefault(x => x.Id == Id) is AbilityBoostValue attributeValue
					? attributeValue.Selected
					: [],
				Possible = Possible,
			};
		}
	}
}
