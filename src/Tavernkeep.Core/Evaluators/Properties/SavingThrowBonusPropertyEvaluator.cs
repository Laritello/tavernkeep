using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Evaluators.Modifiers;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class SavingThrowBonusPropertyEvaluator(SavingThrow savingThrow) : IValueEvaluator<int>
	{
		private readonly SavingThrow _savingThrow = savingThrow;
		private readonly Character _character = savingThrow.Owner;
		private readonly ModifierEvaluator _modifierEvaluator = new(savingThrow.Owner, savingThrow.Name);

		public int Value => Calculate();

		private int Calculate()
		{
			return _savingThrow.Ability.Modifier + _savingThrow.Proficiency.GetProficiencyBonus(_character) + _modifierEvaluator.Value;
		}
	}
}
