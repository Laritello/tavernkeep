using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Structures
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
