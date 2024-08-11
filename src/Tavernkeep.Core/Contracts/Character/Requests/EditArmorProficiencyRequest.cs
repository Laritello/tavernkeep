using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Character.Requests
{
	public class EditArmorProficiencyRequest
	{
		/// <summary>
		/// The ID of the character.
		/// </summary>
		public Guid CharacterId { get; set; } = default!;

		/// <summary>
		/// The type of the armor.
		/// </summary>
		public ArmorType Type { get; set; } = default!;

		/// <summary>
		/// The proficiency for the armor.
		/// </summary>
		public Proficiency Proficiency { get; set; } = default!;
	}
}
