using MediatR;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Application.UseCases.Characters.Commands.EditArmor
{
	public class EditArmorCommand(Guid initiatorId, Guid characterId, ArmorType type, int bonus, bool hasDexterityCap, int dexterityCap, Dictionary<ArmorType, Proficiency> proficiencies) : IRequest
	{
		public Guid InitiatorId { get; set; } = initiatorId;
		public Guid CharacterId { get; set; } = characterId;

		/// <summary>
		/// Currently equipped armor type.
		/// </summary>
		public ArmorType Type { get; set; } = type;

		/// <summary>
		/// Amror bonus for currently equipped armor.
		/// </summary>
		public int Bonus { get; set; } = bonus;

		/// <summary>
		/// Shows whether or not currenlty equipped armor has a dexterity cap.
		/// </summary>
		public bool HasDexterityCap { get; set; } = hasDexterityCap;

		/// <summary>
		/// Dexterity cap for currently equipped armor.
		/// </summary>
		public int DexterityCap { get; set; } = dexterityCap;

		/// <summary>
		/// Character's proficiencies for different types of armor.
		/// </summary>
		public Dictionary<ArmorType, Proficiency> Proficiencies { get; set; } = proficiencies;
	}
}
