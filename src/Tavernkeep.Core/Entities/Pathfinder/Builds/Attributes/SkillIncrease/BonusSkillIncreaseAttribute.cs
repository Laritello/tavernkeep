using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Values;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.SkillIncrease
{
	public class BonusSkillIncreaseAttribute : SkillIncreaseAttribute
	{
		public int Amount { get; set; }

		public override BuildAttribute Convert(BuildSnapshot snapshot, ConversionParameters? parameters = null) => Convert(snapshot.Values, parameters);

		public override BuildAttribute Convert(List<BuildValue> values, ConversionParameters? parameters = null)
		{
			return new BonusSkillIncreaseAttribute()
			{
				Id = Id,
				Level = Level,
				Selected = values.FirstOrDefault(x => x.Id == Id) is SkillIncreaseValue value
					? value.Selected
					: [],
				Amount = parameters?["BonusSkillsAmount"] is int amount ? amount : 0
			};
		}
	}
}
