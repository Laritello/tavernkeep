using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	// TODO: Add base repository for types that use name as primary key
	[Table("Ancestries")]
	public class Ancestry : NameEntity
	{
		public string Description { get; set; } = default!;
		public List<LevelProgression> Progression { get; set; }

		public Ancestry()
		{
			Progression = [];
		}
	}
}
