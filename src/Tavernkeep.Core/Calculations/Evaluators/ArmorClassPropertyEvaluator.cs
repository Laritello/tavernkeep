using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Calculations.Evaluators
{
	public class ArmorClassPropertyEvaluator(ArmorClass armorClass) : IPropertyEvaluator<int>
	{
		private readonly ArmorClass _armorClass = armorClass;
		private readonly Character _character = armorClass.Owner;

		public int Value => 10 + _character.Dexterity.Modifier + _armorClass.Proficiencies[ArmorType.Unarmored].GetProficiencyBonus(_character) + CalculateBonus();

		public int CalculateBonus()
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
