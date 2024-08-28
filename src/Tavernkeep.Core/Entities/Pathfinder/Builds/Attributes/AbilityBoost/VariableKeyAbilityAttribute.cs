using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Values;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityBoost
{
	public class VariableKeyAbilityAttribute : AbilityBoostAttribute
	{
		public List<AbilityType> Possible { get; set; } = [];

		public override BuildAttribute Convert(BuildSnapshot snapshot, ConversionParameters? parameters = null) => Convert(snapshot.Values, parameters);

		public override BuildAttribute Convert(List<BuildValue> values, ConversionParameters? parameters = null)
		{
			return new VariableKeyAbilityAttribute()
			{
				Id = Id,
				Level = Level,
				Selected = values.FirstOrDefault(x => x.Id == Id) is AbilityBoostValue attributeValue
					? attributeValue.Selected
					: [],
				Possible = Possible,
			};
		}
	}
}
