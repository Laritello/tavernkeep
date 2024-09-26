using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Parts
{
	public class Ancestry
	{
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public int HitPoints { get; set; } = 0;
		public UnitSize Size { get; set; } = UnitSize.Medium;
		public int Speed { get; set; }
		public List<string> Languagues { get; set; } = ["Common"];
		public List<BuildAttribute> Attributes { get; set; } = [];
	}
}
