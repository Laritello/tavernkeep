using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Evaluators.Modifiers;

namespace Tavernkeep.Core.Entities.Library.Creatures
{
	[Table("Creature")]
	public class Creature : GuidEntity
	{
		public required string Name { get; set; }
		public required string Type { get; set; }
		public int Level { get; set; }
		public ICollection<string> Traits { get; set; } = [];

		public ICollection<IStatblock> Statblocks { get; set; } = [];
	}
}
