using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;

namespace Tavernkeep.Core.Entities.Templates
{
	[Table("Backgrounds")]
	public class BackgroundTemplate : StringEntity
	{
		public string Name { get; set; } = string.Empty;
		public List<BuildAttribute> Attributes { get; set; } = [];
	}
}
