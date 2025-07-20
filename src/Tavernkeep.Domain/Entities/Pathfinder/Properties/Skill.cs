using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Contracts.Interfaces;
using Tavernkeep.Domain.Entities.Snapshots;
using Tavernkeep.Domain.Evaluators.Properties;
using Tavernkeep.Domain.Interfaces;

namespace Tavernkeep.Domain.Entities.Pathfinder.Properties;

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

	public SkillSnapshot AsSnapshot() => new(Name, Type, Proficiency, Bonus);

	#endregion
}
