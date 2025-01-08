using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
	public class EditArmorRequest
	{
		/// <summary>
		/// Currently equipped armor type.
		/// </summary>
		public ArmorType Type { get; set; } = default!;

		/// <summary>
		/// Amror bonus for currently equipped armor.
		/// </summary>
		public int Bonus { get; set; }

		/// <summary>
		/// Shows whether or not currenlty equipped armor has a dexterity cap.
		/// </summary>
		public bool HasDexterityCap { get; set; }

		/// <summary>
		/// Dexterity cap for currently equipped armor.
		/// </summary>
		public int DexterityCap { get; set; }

		/// <summary>
		/// Character's proficiencies for different types of armor.
		/// </summary>
		public Dictionary<ArmorType, Proficiency> Proficiencies { get; set; } = [];
	}
}
