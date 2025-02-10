using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Core.Entities.Encounters
{
	[Table("Encounter")]
	public class Encounter : GuidEntity
	{
		#region Backing fields

		private readonly List<EncounterParticipant> _participants = [];

		#endregion

		#region Constructors

		public Encounter(string name, EncounterStatus status = EncounterStatus.Draft) 
		{
			Name = name;
			Status = status;
		}

		#endregion

		#region Properties

		public required string Name { get; set; }
		public EncounterStatus Status { get; set; }
		public IReadOnlyCollection<EncounterParticipant> Participants => _participants.AsReadOnly();

		#endregion

		#region Methods

		public void AddParticipant(EncounterParticipant participant)
		{
			_participants.Add(participant);
		}

		public void RemoveParticipant(EncounterParticipant participant)
		{
			_participants.Remove(participant);
		}

		#endregion
	}
}
