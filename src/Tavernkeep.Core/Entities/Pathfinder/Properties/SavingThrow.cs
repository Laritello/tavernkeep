using System.Text.Json.Serialization;
using Tavernkeep.Core.Evaluators.Properties;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Snapshots;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class SavingThrow
	{
		#region Backing fields

		private IPropertyEvaluator<int>? _savingThrowBonusEvaluator;

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
		public AbilityType BaseAbility { get; set; }
		public SavingThrowType Type { get; set; }
		public Proficiency Proficiency { get; set; }
		public int Bonus
		{
			get
			{
				_savingThrowBonusEvaluator ??= new SavingThrowBonusPropertyEvaluator(this);
				return _savingThrowBonusEvaluator.Value;
			}
		}

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
