using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Attributes.Base;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Core.Evaluators.Properties
{
	public class SkillProficiencyPropertyEvaluator(Skill skill) : IValueEvaluator<Proficiency>
	{
		private readonly Skill _skill = skill;
		private readonly Character _character = skill.Owner;

		public Proficiency Value => Calculate();

		private Proficiency Calculate()
		{
			var advancementsCount = _character.Build.Attributes
				.Where(x => x.Level <= _character.Level && x is SkillIncreaseAttribute)
				.Cast<SkillIncreaseAttribute>()
				.Where(x => x.Contains(_skill.Type))
				.DistinctBy(x => x.MaxProficiency)
				.Count();

			// Proficiency values relate to the base proficiency bonus
			// That's why we need to multiply the amount of skill increases by 2.
			return (Proficiency)(advancementsCount * 2);
		}
	}
}
