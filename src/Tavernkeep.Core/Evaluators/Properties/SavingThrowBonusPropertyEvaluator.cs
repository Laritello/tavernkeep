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
		private readonly IValueEvaluator<int> _modifierEvaluator = new ModifierEvaluator(savingThrow.Owner, savingThrow.Type.ToTarget());

		public int Value => Calculate();

		private int Calculate()
		{
			return _character.GetSavingThrowAbility(_savingThrow.Type).Modifier + _savingThrow.Proficiency.GetProficiencyBonus(_character) + _modifierEvaluator.Value;
		}
	}
}
