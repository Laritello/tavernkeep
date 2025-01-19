using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Evaluators.Properties;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Perception
	{
		#region Backing fields

		private PerceptionBonusPropertyEvaluator? _perceptionBonusEvaluator;

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
		public required Character Owner { get; set; }
		public required Ability AssociatedAbility { get; set; }
		public Proficiency Proficiency { get; set; }
		public int Bonus
		{
			get
			{
				_perceptionBonusEvaluator ??= new PerceptionBonusPropertyEvaluator(this);
				return _perceptionBonusEvaluator.Value;
			}
		}

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
