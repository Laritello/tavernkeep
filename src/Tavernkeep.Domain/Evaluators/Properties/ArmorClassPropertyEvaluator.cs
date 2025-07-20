using Tavernkeep.Domain.Contracts.Interfaces;
using Tavernkeep.Domain.Entities.Pathfinder;
using Tavernkeep.Domain.Entities.Pathfinder.Properties;
using Tavernkeep.Domain.Evaluators.Modifiers;
using Tavernkeep.Domain.Extensions;

namespace Tavernkeep.Domain.Evaluators.Properties;

public class ArmorClassPropertyEvaluator(Armor armorClass) : IValueEvaluator<int>
{
	private readonly Armor _armorClass = armorClass;
	private readonly Character _character = armorClass.Owner;
	private readonly CharacterModifierEvaluator _modifierEvaluator = new(armorClass.Owner, "ArmorClass", "Dexterity", "AllChecks");
	public int Value => Calculate();

	public int Calculate()
	{
		var dexterity = _character.Abilities["Dexterity"];

		int dexterityBonus = _armorClass.Equipped.HasDexterityCap
			? Math.Min(dexterity.Modifier, _armorClass.Equipped.DexterityCap)
			: dexterity.Modifier;

		return 10 + dexterityBonus + _armorClass.Proficiencies[_armorClass.Equipped.Type].GetProficiencyBonus(_character) + _modifierEvaluator.Value;
	}
}
