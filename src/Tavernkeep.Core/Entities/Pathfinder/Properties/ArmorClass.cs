using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Calculations.Managers;
using Tavernkeep.Core.Contracts.Interfaces;

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
		public IPropertyManager Manager => _manager ??= new ArmorClassPropertyManager(this);
		public ArmorProficiencies Proficiencies { get; set; }
		public int Class => Manager.Value;

		#endregion
	}
}
