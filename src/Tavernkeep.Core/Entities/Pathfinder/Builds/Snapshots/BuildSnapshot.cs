using Tavernkeep.Core.Entities.Pathfinder.Builds.Values;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Snapshots
{
	public class BuildSnapshot
	{
		public string Id { get; set; }
		public List<BuildValue> Values { get; set; }
	}
}
