using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Creatures;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Contracts.Creatures
{
	public class CreatureDto
	{
		public Guid Id { get; set; }
		public required string Name { get; set; }
		public int Level { get; set; }
		public UnitSize Size { get; set; }
		public Rarity Rarity { get; set; }
		public required List<string> Traits { get; set; }
	}
}
