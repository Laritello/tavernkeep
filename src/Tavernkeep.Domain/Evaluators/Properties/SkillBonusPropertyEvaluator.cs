using Tavernkeep.Domain.Contracts.Interfaces;
using Tavernkeep.Domain.Entities.Pathfinder;
using Tavernkeep.Domain.Entities.Pathfinder.Properties;
using Tavernkeep.Domain.Evaluators.Modifiers;
using Tavernkeep.Domain.Extensions;

namespace Tavernkeep.Domain.Evaluators.Properties;

public class SkillBonusPropertyEvaluator(Skill skill) : IValueEvaluator<int>
{
	private readonly Skill _skill = skill;
	private readonly Character _character = skill.Owner;
	private readonly CharacterModifierEvaluator _modifierEvaluator = new(skill.Owner, skill.Name, skill.Ability.Name, "SkillChecks", "AllChecks");

	public int Value => Calculate();

	private int Calculate()
	{
		return _skill.Ability.Modifier + _skill.Proficiency.GetProficiencyBonus(_character) + _modifierEvaluator.Value;
	}
}
