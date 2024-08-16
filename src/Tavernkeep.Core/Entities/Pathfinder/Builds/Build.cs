using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Build(Character owner)
	{
		private Dictionary<int, LevelProgression>? _progression;

		public Character Owner { get; init; } = owner;
		public Ancestry Ancestry { get; set; } = Ancestry.Empty;
		public Background Background { get; set; } = Background.Empty;
		public Class Class { get; set; } = Class.Empty;

		public Dictionary<int, LevelProgression> Progression => _progression ??= CollectProgression();

		private Dictionary<int, LevelProgression> CollectProgression()
		{
			Dictionary<int, LevelProgression> result = [];

			for (int level = 1; level <= 20; level++)
			{
				result.Add(level, Ancestry[level] + Background[level] + Class[level]);
			}

			return result;
		}
	}
}
