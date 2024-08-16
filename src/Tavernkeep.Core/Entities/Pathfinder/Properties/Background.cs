using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	[Table("Backgrounds")]
	public class Background : NameEntity
	{
		public string Description { get; set; } = default!;
		public List<LevelProgression> Progression { get; set; }

		public Background()
		{
			Progression = [];
		}

		public LevelProgression this[int level] => Progression.FirstOrDefault(p => p.Level == level) ?? new(level);
		public static Background Empty => new();
	}
}
