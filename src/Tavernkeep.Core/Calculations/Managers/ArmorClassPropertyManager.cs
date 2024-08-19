using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Core.Calculations.Managers
{
	public class ArmorClassPropertyManager(Character character) : IPropertyManager
	{
		private readonly Character _character = character;

		public int GetBonus()
		{
			var conditions = _character.Conditions;

			var conditionModifiers = _character.Conditions
				.SelectMany(x => x.CollectModifiers(_character))
				.Where(x => x.Targets.Contains(ModifierTarget.ArmorClass))
				.ToList();

			var activeBonus = conditionModifiers.Where(x => x.IsBonus).MaxBy(x => x.Value);
			var activePenalty = conditionModifiers.Where(x => x.IsPenalty).MaxBy(x => x.Value);

			var total = (activeBonus != null ? activeBonus.Value : 0) + (activePenalty != null ? activePenalty.Value : 0);
			return total;
		}
	}
}
