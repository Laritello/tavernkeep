using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Evaluators.Properties;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	public class Skill
	{
		#region Backing fields

		private IValueEvaluator<int>? _bonusEvaluator;
		private IValueEvaluator<Proficiency>? _proficiencyEvaluator;

		#endregion

		#region Constructors

		public Skill()
		{

		}

		public Skill(Character owner, AbilityType baseAbility, SkillType type)
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
		public SkillType Type { get; set; }
		public Proficiency Proficiency
		{
			get
			{
				_proficiencyEvaluator ??= new SkillProficiencyPropertyEvaluator(this);
				return _proficiencyEvaluator.Value;
			}
		}
		public int Bonus
		{
			get
			{
				_bonusEvaluator ??= new SkillBonusPropertyEvaluator(this);
				return _bonusEvaluator.Value;
			}
		}

		#endregion

		#region Methods

		public SkillSnapshot AsSnapshot()
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
