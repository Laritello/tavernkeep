using System.Text.Json.Serialization;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Core.Contracts.Encounters.Dtos
{
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
	}
}
