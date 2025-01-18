using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Entities.Snapshots
{
	public class SkillSnapshot(string name, Proficiency proficiency, int bonus)
	{
		public string Name { get; set; } = name;
		public Proficiency Proficiency { get; set; } = proficiency;
		public int Bonus { get; set; } = bonus;
	}
}
