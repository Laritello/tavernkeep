namespace Tavernkeep.Domain.Contracts.Encounters.Dtos;

public class CreatureEncounterParticipantDto : EncounterParticipantDto
{
	public required string Statblock { get; set; }
}
