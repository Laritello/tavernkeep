using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Calculations.Managers;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class SavingThrow
	{
		#region Backing fields

		private IPropertyManager _manager;

		#endregion

		#region Constructors

		public SavingThrow()
		{

		}

		public SavingThrow(Character owner, AbilityType baseAbility, SavingThrowType type)
		{
			Owner = owner;
			BaseAbility = baseAbility;
			Type = type;
		}

		#endregion

		#region Properties

		[JsonIgnore]
		public Character Owner { get; set; } = default!;

		[JsonIgnore]
		[NotMapped]
		public IPropertyManager Manager => _manager ??= new SkillPropertyManager(Owner, Type.ToTarget());
		public AbilityType BaseAbility { get; set; }
		public SavingThrowType Type { get; set; }
		public Proficiency Proficiency { get; set; }
		public int Bonus => Owner.GetSavingThrowAbility(Type).Modifier + Proficiency.GetProficiencyBonus(Owner) + Manager.GetBonus();

		#endregion

		#region Methods

		public SavingThrowSnapshot AsSnapshot()
		{
			return new()
			{
				Type = Type,
				Proficiency = Proficiency,
				Bonus = Bonus
			};
		}

		#endregion
	}
}
