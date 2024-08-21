using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class General
	{
		public List<LevelProgression> Progression { get; set; }

		public General()
		{
			Progression = [];
		}

		public LevelProgression this[int level] => Progression.FirstOrDefault(p => p.Level == level) ?? new(level);

		public static General Template => new()
		{
			Progression =
			[
				new(5)
				{
					Advancements = 
					[
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
					]
				},

				new(10)
				{
					Advancements =
					[
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
					]
				},

				new(15)
				{
					Advancements =
					[
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
					]
				},

				new(20)
				{
					Advancements =
					[
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
						AbilityBoostAdvancement.Free,
					]
				},
			]
		};
	}
}
