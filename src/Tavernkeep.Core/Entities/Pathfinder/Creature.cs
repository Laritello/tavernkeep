using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Creatures;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Entities.Pathfinder
{
	[Table("LibraryCreature")]
	public class Creature : GuidEntity
	{
		public required string Name { get; set; }
		public int Level { get; set; }
		public UnitSize Size { get; set; }
		public Rarity Rarity { get; set; }
		public int Perception { get; set; }
		public int ArmorClass { get; set; }
		public required HealthInformation Health { get; set; }
		public required Dictionary<string, int> Abilities { get; set; }
		public required Dictionary<string, int> Skills { get; set; }
		public required Dictionary<string, int> SavingThrows { get; set; }
		public required List<Sense> Senses { get; set; }
		public required List<string> Languages { get; set; }
		public required List<string> Traits { get; set; }
		public required List<Resistance> Resistances { get; set; }
		public required List<Weakness> Weaknesses { get; set; }
		public required List<SpeedInformation> Speeds { get; set; }
		public required Dictionary<string, string> Notes { get; set; }
	}
}
