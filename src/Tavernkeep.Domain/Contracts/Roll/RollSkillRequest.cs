using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Roll
{
	public class RollSkillRequest
	{
		public Guid CharacterId { get; set; }
		public required string Name { get; set; }
		public RollType RollType { get; set; }
	}
}
