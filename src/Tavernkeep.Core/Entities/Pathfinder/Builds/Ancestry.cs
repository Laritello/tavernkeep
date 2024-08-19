using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Ancestry
	{
		public string Name { get; set; } = default!;
		public List<string> Tags { get; set; } = [];
		public int HitPoints { get; set; }
		public UnitSize Size { get; set; }
		public int Speed { get; set; }
		public List<string> Languages { get; set; } = [];
		public List<LevelProgression> Progression { get; set; } = [];

		public Ancestry()
		{

		}

		public LevelProgression this[int level] => Progression.FirstOrDefault(p => p.Level == level) ?? new(level);

		public static Ancestry Empty => new();
	}
}
