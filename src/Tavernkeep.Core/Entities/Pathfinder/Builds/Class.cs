using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Class
	{
		public string Name { get; set; } = default!;
		public List<Advancement> Advancements { get; set; } = [];

		public Class()
		{
			
		}
	}
}
