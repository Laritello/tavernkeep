using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Evaluators.Modifiers;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class SkillBonusPropertyEvaluator(Skill skill) : IValueEvaluator<int>
	{
		private readonly Skill _skill = skill;
		private readonly Character _character = skill.Owner;
		private readonly IValueEvaluator<int> _modifierEvaluator = new ModifierEvaluator(skill.Owner, skill.Type.ToTarget());

		public int Value => Calculate();

		private int Calculate()
		{
			return _character.GetSkillAbility(_skill.Type).Modifier + _skill.Proficiency.GetProficiencyBonus(_character) + _modifierEvaluator.Value;
		}
	}
}
