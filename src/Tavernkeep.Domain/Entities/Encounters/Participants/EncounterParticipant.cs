using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Base;

namespace Tavernkeep.Domain.Entities.Encounters.Participants;

[Table("EncounterParticipant")]
public abstract class EncounterParticipant : GuidEntity
{
	public abstract EncounterParticipantType Type { get; }
	public required Encounter Encounter { get; set; }
	public string? GroupName { get; set; }
	public int? Initiative { get; set; }
	public int Ordinal { get; set; }
}
