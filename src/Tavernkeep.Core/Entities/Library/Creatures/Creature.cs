using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Entities.Library.Creatures
{
	[Table("Creature")]
	public class Creature : GuidEntity
	{
		public required string Name { get; set; }
		public required string Type { get; set; }
		public int Level { get; set; }
		public int Health { get; set; }
		public ICollection<string> Traits { get; set; } = [];
		public required string Statblock { get; set; }

		[NotMapped]
		public int ArmorClass { get; set; }

		[NotMapped]
		public int Perception { get; set; }

		[NotMapped]
		public IReadOnlyDictionary<string, int> SavingThrows { get; set; } = null!;
	}
}
