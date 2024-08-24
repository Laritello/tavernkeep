using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Entities.Pathfinder.Backgrounds
{
	[Table("Backgrounds")]
	public class BackgroundMetadata : NameEntity
	{
		public string Description { get; set; } = default!;
		public List<Advancement> Advancements { get; set; }

		public BackgroundMetadata()
		{
			Advancements = [];
		}
	}
}
