using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Pathfinder.Builds.Advancements
{
	public class SkillIncreaseAdvancement : Advancement
	{
		public bool IsFree { get; set; }
		public List<SkillType> Possible { get; set; } = [];
		public SkillType? Selected { get; set; }
		public Proficiency MaximumAllowedProficiency { get; set; } = Proficiency.Trained;
		public override string ToString() => $"AbilityBoostAdvancement: {Selected} ({string.Join(',', Possible)})";
	}
}
