using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Structures
{
	public class EquippedArmor
	{
		public EquippedArmor()
		{
			Type = ArmorType.Unarmored;
			Bonus = 0;
			HasDexterityCap = false;
		}

		public ArmorType Type { get; set; }
		public int Bonus { get; set; }
		public bool HasDexterityCap { get; set; }
		public int DexterityCap { get; set; }
	}
}
