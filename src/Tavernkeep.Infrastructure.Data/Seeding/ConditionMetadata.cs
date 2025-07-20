using Tavernkeep.Domain.Contracts.Structures;

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

		public List<RelatedConditionMetadata> Related { get; set; } = [];
		public Dictionary<string, Modifier> Modifiers { get; set; } = [];

		#endregion
	}

	internal class RelatedConditionMetadata
	{
		public required string Name { get; set; }
		public int? Level { get; set; }
	}
}
