using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Roll
{
	public class RollSkillRequest
	{
		public Guid CharacterId { get; set; }
		public string SkillType { get; set; }
		public RollType RollType { get; set; }
	}
}
