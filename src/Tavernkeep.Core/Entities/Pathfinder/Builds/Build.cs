using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Parts;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds
{
	public class Build
	{
		public Build()
		{
			Level = 1;

			General = new();
			Ancestry = new();
			Class = new();
		}

		public int Level { get; set; }
		public General General { get; set; }
		public Ancestry Ancestry { get; set; }
		public Class Class { get; set; }

		public List<BuildAttribute> Attributes => [.. General?.Attributes ?? [], .. Ancestry?.Attributes ?? [], .. Class?.Attributes ?? []];
	}
}
