using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Calculations.Managers
{
	public class AbilityPropertyManager(Ability ability) : IPropertyManager
	{
		private readonly Ability _ability = ability;
		private readonly Character _character = ability.Owner;

		public int Value => CalculateScore();

		private int CalculateScore()
		{
			var progression = _character.Build.Progression;

			int boostAmount = 0;
			int flawAmount = 0;

			for (int level = 1; level <= _character.Build.Level; level++)
			{
				var levelProgression = progression[level];
				boostAmount += levelProgression.Advancements.Where(x => (x is AbilityBoostAdvancement advancement) && advancement.Selected == _ability.Type).Count();
				flawAmount += levelProgression.Advancements.Where(x => (x is AbilityFlawAdvancement advancement) && advancement.Selected == _ability.Type).Count();
			}

			int score = 10 - 2 * flawAmount;
			
			for (int i = 0; i < boostAmount; i++)
			{
				score += score < 18 ? 2 : 1;
			}

			return score;
		}
	}
}
