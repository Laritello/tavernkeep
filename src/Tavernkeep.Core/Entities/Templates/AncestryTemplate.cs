using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;

namespace Tavernkeep.Core.Entities.Templates
{
	[Table("Ancestries")]
	public class AncestryTemplate : StringEntity
	{
		public string Name { get; set; } = string.Empty;
		public List<string> Traits { get; set; } = [];
		public string Description { get; set; } = string.Empty;
		public int HitPoints { get; set; } = 0;
		public UnitSize Size { get; set; } = UnitSize.Medium;
		public int Speed { get; set; }
		public List<string> Languagues { get; set; } = ["Common"];
		public List<BuildAttribute> Attributes { get; set; } = [];
	}
}
