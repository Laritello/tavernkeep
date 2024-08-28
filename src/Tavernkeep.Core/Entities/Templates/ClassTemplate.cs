using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;

namespace Tavernkeep.Core.Entities.Templates
{
	[Table("Classes")]
	public class ClassTemplate : StringEntity
	{
		public string Name { get; set; } = string.Empty;
		public int SkillBaseAmount { get; set; }
		public List<BuildAttribute> Attributes { get; set; } = [];
	}
}
