using System.Text.Json.Serialization;
using Tavernkeep.Domain.Contracts.Character.Dtos;
using Tavernkeep.Domain.Contracts.Conditions.Dtos;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Encounters.Participants;

namespace Tavernkeep.Domain.Contracts.Encounters.Dtos;

[JsonDerivedType(typeof(CharacterEncounterParticipantDto), typeDiscriminator: nameof(CharacterEncounterParticipant))]
[JsonDerivedType(typeof(CreatureEncounterParticipantDto), typeDiscriminator: nameof(CreatureEncounterParticipant))]
public abstract class EncounterParticipantDto
{
	public Guid Id { get; set; }
	public required string Name { get; set; }
	public Guid EntityId { get; set; }
	public EncounterParticipantType Type { get; set; }
	public int? Initiative { get; set; }
	public int ArmorClass { get; set; }
	public int Perception { get; set; }
	public required HealthDto Health { get; set; }
	public required Dictionary<string, int> SavingThrows { get; set; }
	public required ICollection<ConditionShortDto> Conditions { get; set; }
}
