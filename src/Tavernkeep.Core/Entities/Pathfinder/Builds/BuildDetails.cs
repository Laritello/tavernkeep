using Tavernkeep.Core.Entities.Pathfinder.Builds.Parts;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class BuildDetails
	{
		public int Level { get; set; }
		public General General { get; set; }
		public AncestryTemplate Ancestry { get; set; }
		public BackgroundTemplate Background { get; set; }
		public ClassTemplate Class { get; set; }
	}
}
