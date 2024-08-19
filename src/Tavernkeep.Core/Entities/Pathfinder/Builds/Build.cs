using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Build
	{
		[NotMapped]
		private Dictionary<int, LevelProgression>? _progression;

		[JsonIgnore]
		public Character Owner { get; init; }
		public int Level { get; set; } = 1;
		public Ancestry Ancestry { get; set; } = Ancestry.Empty;
		public Background Background { get; set; } = Background.Empty;
		public Class Class { get; set; } = Class.Empty;

		[NotMapped]
		public Dictionary<int, LevelProgression> Progression => _progression ??= CollectProgression();

		public Build()
		{

		}

		public Build(Character owner)
		{
			Owner = owner;
		}

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
