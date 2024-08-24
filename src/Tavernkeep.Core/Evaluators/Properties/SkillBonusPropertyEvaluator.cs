using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class SkillBonusPropertyEvaluator(Skill skill) : IPropertyEvaluator<int>
	{
		private readonly Skill _skill = skill;
		private readonly Character _character = skill.Owner;

		public int Value => Calculate();

		private int Calculate()
		{
			var target = _skill.Type.ToTarget();
			var conditions = _character.Conditions;

			var conditionModifiers = _character.Conditions
				.SelectMany(x => x.CollectModifiers(_character))
				.Where(x => x.Targets.Contains(target))
				.ToList();

			var activeBonus = conditionModifiers.Where(x => x.IsBonus).MaxBy(x => x.Value);
			var activePenalty = conditionModifiers.Where(x => x.IsPenalty).MaxBy(x => x.Value);

			var totalBonus = (activeBonus != null ? activeBonus.Value : 0) + (activePenalty != null ? activePenalty.Value : 0);
			
			return _character.GetSkillAbility(_skill.Type).Modifier + _skill.Proficiency.GetProficiencyBonus(_character) + totalBonus;
		}
	}
}
