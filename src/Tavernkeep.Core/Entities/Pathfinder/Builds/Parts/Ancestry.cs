using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Parts
{
	public class Ancestry
	{
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public List<BuildAttribute> Attributes { get; set; } = [];
	}
}
