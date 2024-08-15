using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder.Modifiers.Managers;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class SavingThrow : IModifiable
	{
		#region Backing fields

		private IModifierManager _manager;

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
		public IModifierManager Manager => _manager ??= new GeneralModifierManager(Owner, Type.ToTarget());
		public AbilityType BaseAbility { get; set; }
		public SavingThrowType Type { get; set; }
		public Proficiency Proficiency { get; set; }
		public int Bonus => Owner.GetSavingThrowAbility(Type).Modifier + Proficiency.GetProficiencyBonus(Owner) + Manager.GetSummary().Total;

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
