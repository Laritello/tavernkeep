using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Ancestry
	{
		public string Name { get; set; } = default!;
		public List<string> Tags { get; set; } = [];
		public int HitPoints { get; set; }
		public UnitSize Size { get; set; }
		public int Speed { get; set; }
		public List<string> Languages { get; set; } = [];
		public List<Advancement> Advancements { get; set; } = [];

		public Ancestry()
		{

		}
	}
}
