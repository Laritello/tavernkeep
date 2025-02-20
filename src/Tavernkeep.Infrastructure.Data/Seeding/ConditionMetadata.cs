using Tavernkeep.Core.Contracts.Structures;

namespace Tavernkeep.Infrastructure.Data.Seeding
{
	internal class ConditionMetadata
	{
		#region Constructors

		public ConditionMetadata() { }

		#endregion

		#region Properties
		public required string Name { get; set; }
		public required string Description { get; set; }
		public bool HasLevels { get; set; }

		public List<string> Related { get; set; } = [];
		public Dictionary<string, Modifier> Modifiers { get; set; } = [];

		#endregion
	}
}
