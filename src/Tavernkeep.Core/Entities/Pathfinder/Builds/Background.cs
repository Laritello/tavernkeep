using Tavernkeep.Core.Entities.Pathfinder.Backgrounds;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Background
	{
		public string Name { get; set; } = default!;
		public List<Advancement> Advancements { get; set; } = [];

		public Background()
		{
			
		}
	}
}
