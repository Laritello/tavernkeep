using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Structures;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities.Library.Conditions
{
	[Table("Condition")]
	public class Condition : StringEntity
	{
		#region Constructors

		public Condition() { }

		#endregion

		#region Properties

		public required string Description { get; set; }
		public bool HasLevels { get; set; }

		public ICollection<ConditionRelated> Related { get; set; } = [];
		public Dictionary<string, Modifier> Modifiers { get; set; } = [];

		#endregion
	}
}
