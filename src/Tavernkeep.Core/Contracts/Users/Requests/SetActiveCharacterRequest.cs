namespace Tavernkeep.Core.Contracts.Users.Requests
{
	public class SetActiveCharacterRequest
	{
		public Guid UserId { get; set; }
		public Guid CharacterId { get; set; }
	}
}
