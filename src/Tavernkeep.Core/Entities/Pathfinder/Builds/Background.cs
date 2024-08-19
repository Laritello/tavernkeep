using Tavernkeep.Core.Entities.Pathfinder.Backgrounds;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Background
	{
		public string Name { get; set; } = default!;
		public List<LevelProgression> Progression { get; set; }

		public Background()
		{
			Progression = [];
		}

		public LevelProgression this[int level] => Progression.FirstOrDefault(p => p.Level == level) ?? new(level);
		public static Background Empty => new();
	}
}
