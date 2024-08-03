using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Modifiers;

namespace Tavernkeep.Core.Entities.Conditions
{
	public class ConditionMetadata : Entity
	{
		#region Constructors

		public ConditionMetadata() { }

		#endregion

		#region Properties

		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public bool HasLevels { get; set; }
		public int Level { get; set; }

		public List<Condition> Related { get; set; } = [];
		public List<Modifier> Modifiers { get; set; } = [];

		#endregion
	}
}
