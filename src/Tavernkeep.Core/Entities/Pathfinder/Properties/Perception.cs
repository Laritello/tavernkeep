using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Calculations.Managers;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Perception
	{
		#region Backing fields

		private IPropertyManager _manager;

		#endregion

		#region Constructors

		public Perception()
		{

		}

		public Perception(Character owner)
		{
			Owner = owner;
		}

		#endregion

		#region Properties

		[JsonIgnore]
		public Character Owner { get; set; } = default!;

		[JsonIgnore]
		[NotMapped]
		public IPropertyManager Manager => _manager ??= new SkillPropertyManager(Owner, ModifierTarget.Perception);
		public Proficiency Proficiency { get; set; }
		public int Bonus => Owner.Wisdom.Modifier + Proficiency.GetProficiencyBonus(Owner) + Manager.GetBonus();

		#endregion

		#region Methods

		public PerceptionSnapshot AsSnapshot()
		{
			return new()
			{
				Proficiency = Proficiency,
				Bonus = Bonus
			};
		}

		#endregion
	}
}
