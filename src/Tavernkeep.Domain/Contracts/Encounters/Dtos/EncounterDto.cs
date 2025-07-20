using Tavernkeep.Domain.Contracts.Enums;

namespace Tavernkeep.Domain.Contracts.Encounters.Dtos;

public class EncounterDto
{
	public Guid Id { get; set; }
	public required string Name { get; set; }
	public EncounterStatus Status { get; set; }
	public int RoundNumber { get; set; }
	public int CurrentTurnIndex { get; set; }
	public long CreatedAt { get; set; }
	public required ICollection<EncounterParticipantDto> Participants { get; set; }
}
