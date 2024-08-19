using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Calculations.Managers;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class ArmorClass
	{
		#region Backing fields

		private IPropertyManager _manager;

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
		public IPropertyManager Manager => _manager ??= new ArmorClassPropertyManager(Owner);
		public ArmorProficiencies Proficiencies { get; set; }
		public int Class => 10 + Owner.Dexterity.Modifier + Proficiencies[ArmorType.Unarmored].GetProficiencyBonus(Owner) + Manager.GetBonus();

		#endregion
	}
}
