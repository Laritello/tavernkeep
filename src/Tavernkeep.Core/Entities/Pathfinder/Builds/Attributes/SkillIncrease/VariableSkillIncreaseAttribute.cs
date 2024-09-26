using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Conversion;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Values;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.SkillIncrease
{
	public class VariableSkillIncreaseAttribute : SkillIncreaseAttribute
	{
		public List<SkillType> Possible { get; set; } = [];
		public int Amount { get; set; }

		public override BuildAttribute Convert(BuildSnapshot snapshot, ConversionParameters? parameters = null) => Convert(snapshot.Values, parameters);

		public override BuildAttribute Convert(List<BuildValue> values, ConversionParameters? parameters = null)
		{
			return new VariableSkillIncreaseAttribute()
			{
				Id = Id,
				Level = Level,
				Amount = Amount,
				Selected = values.FirstOrDefault(x => x.Id == Id) is SkillIncreaseValue value
				? value.Selected
				: [],
				Possible = Possible,
			};
		}

		public override void Restore(BuildSnapshot snapshot, ConversionParameters? parameters = null) => Restore(snapshot.Values, parameters);

		public override void Restore(List<BuildValue> values, ConversionParameters? parameters = null)
		{
			Selected = values.FirstOrDefault(x => x.Id == Id) is SkillIncreaseValue value
				? value.Selected
				: [];
		}

		public override BuildValue Snapshot()
		{
			return new SkillIncreaseValue()
			{
				Id = Id,
				Selected = Selected,
			};
		}
	}
}
