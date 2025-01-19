using Tavernkeep.Core.Contracts.Enums;

namespace Tavernkeep.Core.Contracts.Roll
{
	public class RollSavingThrowRequest
	{
		public Guid CharacterId { get; set; }
		public string SavingThrowType { get; set; }
		public RollType RollType { get; set; }
	}
}
