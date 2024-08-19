using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds;

namespace Tavernkeep.Core.Entities.Pathfinder.Backgrounds
{
	[Table("Backgrounds")]
	public class BackgroundMetadata : NameEntity
	{
		public string Description { get; set; } = default!;
		public List<LevelProgression> Progression { get; set; }

		public BackgroundMetadata()
		{
			Progression = [];
		}

		public LevelProgression this[int level] => Progression.FirstOrDefault(p => p.Level == level) ?? new(level);
		public static BackgroundMetadata Empty => new();
	}
}
