using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements;

namespace Tavernkeep.Core.Builders
{
	public class AbilityBoostAdvancementBuilder
	{
		private AbilityBoostAdvancement _advancement;

		public AbilityBoostAdvancementBuilder()
		{
			_advancement = new AbilityBoostAdvancement();
		}

		public AbilityBoostAdvancementBuilder WithLevel(int level)
		{
			_advancement.Level = level;
			return this;
		}

		public AbilityBoostAdvancementBuilder Free()
		{
			_advancement.Possible = [.. Enum.GetValues<AbilityType>()];
			return this;
		}

		public AbilityBoostAdvancement Build()
		{
			return _advancement;
		}
	}
}
