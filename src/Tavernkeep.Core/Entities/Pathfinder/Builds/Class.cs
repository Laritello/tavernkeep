namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Class
	{
		public string Name { get; set; } = default!;
		public List<LevelProgression> Progression { get; set; }

		public Class()
		{
			Progression = [];
		}

		public LevelProgression this[int level] => Progression.FirstOrDefault(p => p.Level == level) ?? new(level);

		public static Class Empty => new();
	}
}
