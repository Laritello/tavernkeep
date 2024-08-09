using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Contracts.Structures;
using Tavernkeep.Core.Entities.Pathfinder.Modifiers.Managers;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities.Pathfinder
{
	public class ArmorClass : IModifiable
	{
		#region Backing fields

		private IModifierManager _manager;

		#endregion

		#region Constructors

		public ArmorClass()
		{

		}

		public ArmorClass(Character character)
		{
			Owner = character;
			Proficiencies = new();
		}

		#endregion

		#region Properties

		[JsonIgnore]
		public Character Owner { get; set; } = default!;

		[JsonIgnore]
		[NotMapped]
		public IModifierManager Manager => _manager ??= new GeneralModifierManager(Owner, ModifierTarget.ArmorClass);
		public ArmorProficiencies Proficiencies { get; set; }
		public int Class => 10 + Owner.Dexterity.Modifier + Proficiencies[ArmorType.Unarmored].GetProficiencyBonus(Owner) + Manager.GetSummary().Total;

		#endregion
	}
}
