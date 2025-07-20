using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Contracts.Structures;

namespace Tavernkeep.Domain.Contracts.Character.Dtos
{
	public class ArmorDto
	{
		public int Class { get; set; }
		public EquippedArmor Equipped { get; set; } = default!;
		public Dictionary<ArmorType, Proficiency> Proficiencies { get; set; } = [];
	}
}
