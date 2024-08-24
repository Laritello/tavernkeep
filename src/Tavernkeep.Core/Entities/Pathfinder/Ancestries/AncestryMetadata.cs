using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Ancestries
{
	[Table("Ancestries")]
	public class AncestryMetadata : NameEntity
	{
		public List<string> Traits { get; set; }
		public string Description { get; set; } = default!;
		public int HitPoints { get; set; }
		public UnitSize Size { get; set; }
		public int Speed { get; set; }
		public List<string> Languages { get; set; }
		public List<Advancement> Advancements { get; set; }

		public AncestryMetadata()
		{
			Advancements = [];
			Traits = [];
			Languages = [];
		}
	}
}
