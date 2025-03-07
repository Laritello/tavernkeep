using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;

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
		public ICollection<IStatblock> Blocks { get; set; } = [];

		public int Perception => 0;
		public int ArmorClass => 0;
		public Dictionary<string, int> SavingThrows = [];

		public int GetSkillBonus(string skillName) => 0;
	}
}
