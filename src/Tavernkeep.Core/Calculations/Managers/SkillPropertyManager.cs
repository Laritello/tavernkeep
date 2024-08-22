﻿using Tavernkeep.Core.Contracts.Interfaces;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;
using Tavernkeep.Core.Extensions;

namespace Tavernkeep.Core.Calculations.Managers
{
	public class SkillPropertyManager(Skill skill) : IPropertyManager
	{
		private readonly Skill _skill = skill;
		private readonly Character _character = skill.Owner;

		public int Value => _character.GetSkillAbility(_skill.Type).Modifier + _skill.Proficiency.GetProficiencyBonus(_character) + CalculateBonus();

		private int CalculateBonus()
		{
			var target = _skill.Type.ToTarget();
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
