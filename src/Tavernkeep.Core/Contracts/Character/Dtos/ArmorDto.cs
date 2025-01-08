using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Structures;

namespace Tavernkeep.Core.Contracts.Character.Dtos
{
	public class ArmorDto
	{
		public int Class { get; set; }
		public EquippedArmor Equipped { get; set; } = default!;
		public Dictionary<ArmorType, Proficiency> Proficiencies { get; set; } = [];
	}
}
