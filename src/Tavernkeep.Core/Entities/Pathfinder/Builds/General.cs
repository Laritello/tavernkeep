using Tavernkeep.Core.Builders;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class General
	{
		public List<Advancement> Advancements { get; set; }

		public General()
		{
			Advancements =
			[
				new AbilityBoostAdvancementBuilder().WithLevel(1).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(1).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(1).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(1).Free().Build(),

				new AbilityBoostAdvancementBuilder().WithLevel(5).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(5).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(5).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(5).Free().Build(),

				new AbilityBoostAdvancementBuilder().WithLevel(10).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(10).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(10).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(10).Free().Build(),

				new AbilityBoostAdvancementBuilder().WithLevel(15).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(15).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(15).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(15).Free().Build(),

				new AbilityBoostAdvancementBuilder().WithLevel(20).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(20).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(20).Free().Build(),
				new AbilityBoostAdvancementBuilder().WithLevel(20).Free().Build(),
			];
		}
	}
}
