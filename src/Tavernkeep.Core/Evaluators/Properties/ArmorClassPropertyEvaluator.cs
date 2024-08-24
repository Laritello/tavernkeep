using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Evaluators.Modifiers;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class ArmorClassPropertyEvaluator(ArmorClass armorClass) : IValueEvaluator<int>
	{
		private readonly ArmorClass _armorClass = armorClass;
		private readonly Character _character = armorClass.Owner;
		private readonly IValueEvaluator<int> _modifierEvaluator = new ModifierEvaluator(armorClass.Owner, ModifierTarget.ArmorClass);
		public int Value => Calculate();

		public int Calculate()
		{
			return 10 + _character.Dexterity.Modifier + _armorClass.Proficiencies[ArmorType.Unarmored].GetProficiencyBonus(_character) + _modifierEvaluator.Value;
		}
	}
}
