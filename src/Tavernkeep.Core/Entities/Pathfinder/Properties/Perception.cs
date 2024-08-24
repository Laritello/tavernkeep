using System.Text.Json.Serialization;
using Tavernkeep.Core.Evaluators.Properties;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Snapshots;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Perception
	{
		#region Backing fields

		private IValueEvaluator<int>? _perceptionBonusEvaluator;

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
