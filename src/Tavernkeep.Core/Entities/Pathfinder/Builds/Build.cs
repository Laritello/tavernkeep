using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Pathfinder.Ancestries;
using Tavernkeep.Core.Entities.Pathfinder.Backgrounds;
using Tavernkeep.Core.Entities.Pathfinder.Classes;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
    public class Build
	{
		private Dictionary<int, LevelProgression>? _progression;

		[JsonIgnore]
		public Character Owner { get; init; }
		[NotMapped]
		public AncestryMetadata Ancestry { get; set; } = AncestryMetadata.Empty;
		[NotMapped]
		public BackgroundMetadata Background { get; set; } = BackgroundMetadata.Empty;
		[NotMapped]
		public ClassMetadata Class { get; set; } = ClassMetadata.Empty;

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
