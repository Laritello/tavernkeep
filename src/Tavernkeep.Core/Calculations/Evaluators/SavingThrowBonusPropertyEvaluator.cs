using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Calculations.Evaluators
{
	public class SavingThrowBonusPropertyEvaluator(SavingThrow savingThrow) : IPropertyEvaluator<int>
	{
		private readonly SavingThrow _savingThrow = savingThrow;
		private readonly Character _character = savingThrow.Owner;

		public int Value => Calculate();

		private int Calculate()
		{
			var target = _savingThrow.Type.ToTarget();
			var conditions = _character.Conditions;

			var conditionModifiers = _character.Conditions
				.SelectMany(x => x.CollectModifiers(_character))
				.Where(x => x.Targets.Contains(target))
				.ToList();

			var activeBonus = conditionModifiers.Where(x => x.IsBonus).MaxBy(x => x.Value);
			var activePenalty = conditionModifiers.Where(x => x.IsPenalty).MaxBy(x => x.Value);

			var total = (activeBonus != null ? activeBonus.Value : 0) + (activePenalty != null ? activePenalty.Value : 0);

			return _character.GetSavingThrowAbility(_savingThrow.Type).Modifier + _savingThrow.Proficiency.GetProficiencyBonus(_character) + total;
		}
	}
}
