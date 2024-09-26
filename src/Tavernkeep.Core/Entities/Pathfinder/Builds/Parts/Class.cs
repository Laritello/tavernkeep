using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Parts
{
	public class Class
	{
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public int HitPoints { get; set; } = 0;
		public List<BuildAttribute> Attributes { get; set; } = [];
	}
}
