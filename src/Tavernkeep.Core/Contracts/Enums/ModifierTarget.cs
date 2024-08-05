using System.Text.Json.Serialization;

namespace Tavernkeep.Core.Contracts.Enums
{
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum ModifierTarget
	{
		// Skills
		Acrobatics,
		Arcana,
		Athletics,
		Crafting,
		Deception,
		Diplomacy,
		Intimidation,
		Medicine,
		Nature,
		Occultism,
		Performance,
		Religion,
		Society,
		Stealth,
		Survival,
		Thievery,

		// Custom Skill
		Lore,

		// Saves
		Fortitude,
		Reflex,
		Will,

		// Senses
		Perception,

		// Defenses
		ArmorClass,

		// Offenses
		MeleeAttack,
		RangedAttack,
		SpellAttack,
		DifficultyClass,

		// Speeds
		Speed,
		BurrowSpeed,
		ClimbSpeed,
		FlySpeed,
		SwimSpeed,

		// Initiative
		InitiativePerception,
		InitiativeStealth,

		// Health
		CurrentHealth,
		TotalHealth,
	}
}
