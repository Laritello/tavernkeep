using Tavernkeep.Core.Entities.Conditions;
using Tavernkeep.Core.Entities.Modifiers;

namespace Tavernkeep.Core.Contracts.Conditions.Dtos
{
	public class ConditionMetadataDto
	{
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public bool HasLevels { get; set; }
		public int Level { get; set; }

		public List<Condition> Related { get; set; } = [];
		public List<Modifier> Modifiers { get; set; } = [];
	}
}
