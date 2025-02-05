using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Snapshots;
using Tavernkeep.Core.Evaluators.Properties;
using Tavernkeep.Core.Interfaces;

namespace Tavernkeep.Core.Entities.Pathfinder.Properties
{
	[Table("CharacterSkill")]
	[PrimaryKey(nameof(Id))]
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

		public Guid Id { get; set; }
		public required Character Owner { get; set; }

		public string Name { get; set; }
		public SkillType Type { get; set; }
		public required Ability Ability { get; set; }
		public Proficiency Proficiency { get; set; }
		public bool Pinned { get; set; }
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
