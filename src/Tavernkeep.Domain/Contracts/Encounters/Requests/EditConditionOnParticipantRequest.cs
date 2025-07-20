namespace Tavernkeep.Domain.Contracts.Encounters.Requests
{
	public class EditConditionOnParticipantRequest
	{
		public required string Name { get; set; }
		public int Level { get; set; }
	}
}
