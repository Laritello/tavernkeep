using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Evaluators.Modifiers;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class ArmorClassPropertyEvaluator(Armor armorClass) : IValueEvaluator<int>
	{
		private readonly Armor _armorClass = armorClass;
		private readonly Character _character = armorClass.Owner;
		private readonly ModifierEvaluator _modifierEvaluator = new(armorClass.Owner, ModifierTarget.ArmorClass);
		public int Value => Calculate();

		public int Calculate()
		{
			int dexterityBonus = _armorClass.Equipped.HasDexterityCap 
				? Math.Min(_character.Dexterity.Modifier, _armorClass.Equipped.DexterityCap) 
				: _character.Dexterity.Modifier;

			return 10 + dexterityBonus + _armorClass.Proficiencies[_armorClass.Equipped.Type].GetProficiencyBonus(_character) + _modifierEvaluator.Value;
		}
	}
}
