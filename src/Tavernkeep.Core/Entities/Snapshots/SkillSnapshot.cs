using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Snapshots
{
	public class SkillSnapshot(string name, SkillType type, Proficiency proficiency, int bonus)
	{
		public string Name { get; set; } = name;
		public SkillType Type { get; set; } = type;
		public Proficiency Proficiency { get; set; } = proficiency;
		public int Bonus { get; set; } = bonus;
	}
}
