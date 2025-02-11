using System.Text.Json.Serialization;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Core.Contracts.Encounters.Dtos
{
	[JsonDerivedType(typeof(CharacterEncounterParticipantDto), typeDiscriminator: nameof(CharacterEncounterParticipant))]
	public abstract class EncounterParticipantDto
	{
		public Guid Id { get; set; }
		public int? Initiative { get; set; }
		public int Ordinal { get; set; }
	}
}
