using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds;

namespace Tavernkeep.Core.Entities.Pathfinder.Ancestries
{
	// TODO: Add base repository for types that use name as primary key
	[Table("Ancestries")]
	public class AncestryMetadata : NameEntity
	{
		public List<string> Tags { get; set; }
		public string Description { get; set; } = default!;
		public int HitPoints { get; set; }
		public UnitSize Size { get; set; }
		public int Speed { get; set; }
		public List<string> Languages { get; set; }
		public List<LevelProgression> Progression { get; set; }

		public AncestryMetadata()
		{
			Progression = [];
			Tags = [];
			Languages = [];
		}
	}
}
