using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Evaluators.Properties;
using Tavernkeep.Core.Interfaces;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	[Table("CharacterSkill")]
	public class Skill : INamedProperty
	{
		#region Backing fields

		private IValueEvaluator<int>? _bonusEvaluator;

		#endregion

		#region Constructors

		public Skill(string name, Proficiency proficiency, SkillType type)
		{
			Name = name;
			Proficiency = proficiency;
			Type = type;
		}

		#endregion

		#region Properties

		[Key, Column(Order = 0)]
		public required Character Owner { get; set; }

		[Key, Column(Order = 1)]
		public string Name { get; set; }
		public SkillType Type { get; set; }

		public required Ability Ability { get; set; }

		public Proficiency Proficiency { get; set; }

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

		public SkillSnapshot AsSnapshot() => new(Name, Proficiency, Bonus);

		#endregion
	}
}
