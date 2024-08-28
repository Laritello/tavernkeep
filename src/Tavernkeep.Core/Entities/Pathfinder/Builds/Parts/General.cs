using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.AbilityBoost;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Parts
{
	public class General
	{
		public List<BuildAttribute> Attributes { get; set; }

		public General()
		{
			Attributes =
			[
				new VariableAbilityBoostAttribute()
				{
					Id = Guid.Parse("3f54e631-7fba-416d-8803-732ae15cc1da"),
					Level = 1,
					Amount = 4,
					Possible = [.. Enum.GetValues<AbilityType>()],
				},
			];
		}
	}
}
