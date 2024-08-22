using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Calculations.Managers
{
	public class PerceptionPropertyManager(Perception perception) : IPropertyManager
	{
		private readonly Perception _perception = perception;
		private readonly Character _character = perception.Owner;

		public int Value => _character.Wisdom.Modifier + _perception.Proficiency.GetProficiencyBonus(_character) + CalculateBonus();

		private int CalculateBonus()
		{
			var target = ModifierTarget.Perception;
			var conditions = _character.Conditions;

			var conditionModifiers = _character.Conditions
				.SelectMany(x => x.CollectModifiers(_character))
				.Where(x => x.Targets.Contains(target))
				.ToList();

			var activeBonus = conditionModifiers.Where(x => x.IsBonus).MaxBy(x => x.Value);
			var activePenalty = conditionModifiers.Where(x => x.IsPenalty).MaxBy(x => x.Value);

			var total = (activeBonus != null ? activeBonus.Value : 0) + (activePenalty != null ? activePenalty.Value : 0);

			return total;
		}
	}
}
