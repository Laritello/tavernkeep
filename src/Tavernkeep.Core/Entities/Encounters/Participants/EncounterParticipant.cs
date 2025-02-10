using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Entities.Base;

namespace Tavernkeep.Core.Entities.Encounters.Participants
{
	[Table("EncounterParticipant")]
	public abstract class EncounterParticipant : GuidEntity
	{
		public string? GroupName { get; set; }
		public int? Initiative { get; set; }
		public int Ordinal { get; set; }
	}
}
