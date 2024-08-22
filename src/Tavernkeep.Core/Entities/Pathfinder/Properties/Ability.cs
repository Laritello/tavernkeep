using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Calculations.Managers;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Ability
	{
		#region Backing fields

		private IPropertyManager _manager;

		#endregion

		#region Constructors

		public Ability()
		{

		}

		public Ability(Character owner, AbilityType type)
		{
			Owner = owner;
			Type = type;
		}

		#endregion

		#region Properties

		[JsonIgnore]
		public Character Owner { get; set; } = default!;
		[JsonIgnore]
		[NotMapped]
		public IPropertyManager Manager => _manager ??= new AbilityPropertyManager(this);
		public AbilityType Type { get; set; }
		public int Score => Manager.Value;
		public int Modifier => (Score - 10) / 2;

		#endregion
	}
}
