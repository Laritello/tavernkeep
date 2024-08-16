using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	[Table("Classes")]
	public class Class : NameEntity
	{
		public string Description { get; set; } = default!;
		public List<LevelProgression> Progression { get; set; }

		public Class()
		{
			Progression = [];
		}
	}
}
