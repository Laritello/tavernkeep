using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class PerceptionBonusPropertyEvaluator(Perception perception) : IPropertyEvaluator<int>
	{
		private readonly Perception _perception = perception;
		private readonly Character _character = perception.Owner;

		public int Value => Calculate();

		private int Calculate()
		{
			var target = ModifierTarget.Perception;
			var conditions = _character.Conditions;

			var conditionModifiers = _character.Conditions
				.SelectMany(x => x.CollectModifiers(_character))
				.Where(x => x.Targets.Contains(target))
				.ToList();

			var activeBonus = conditionModifiers.Where(x => x.IsBonus).MaxBy(x => x.Value);
			var activePenalty = conditionModifiers.Where(x => x.IsPenalty).MaxBy(x => x.Value);

			var totalBonus = (activeBonus != null ? activeBonus.Value : 0) + (activePenalty != null ? activePenalty.Value : 0);

			return _character.Wisdom.Modifier + _perception.Proficiency.GetProficiencyBonus(_character) + totalBonus;
		}
	}
}
