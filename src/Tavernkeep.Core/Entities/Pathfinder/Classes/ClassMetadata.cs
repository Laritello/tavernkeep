using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Classes
{
	[Table("Classes")]
	public class ClassMetadata : NameEntity
	{
		public List<string> Traits { get; set; }
		public string Description { get; set; } = default!;
		public int HitPoints { get; set; }
		public List<Advancement> Advancements { get; set; }

		public ClassMetadata()
		{
			Traits = [];
			Advancements = [];
		}
	}
}
