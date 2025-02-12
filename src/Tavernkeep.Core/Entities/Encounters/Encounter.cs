using System.ComponentModel.DataAnnotations.Schema;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Core.Entities.Encounters
{
	[Table("Encounter")]
	public class Encounter : GuidEntity
	{
		#region Private fields

		private readonly IComparer<EncounterParticipant> _participantComparer = new OrdinalComparer();
		
		#endregion

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

		public string Name { get; set; }
		public EncounterStatus Status { get; set; }
		public IReadOnlyCollection<EncounterParticipant> Participants => _participants.AsReadOnly();

		#endregion

		#region Methods

		public void AddParticipant(EncounterParticipant participant)
		{
			participant.Ordinal = _participants.Count;
			_participants.Add(participant);
		}

		public void RemoveParticipant(EncounterParticipant participant)
		{
			int indexOf = _participants.IndexOf(participant);
			_participants.Remove(participant);

			// Update ordinal for all the participants after the deleted one
			for (int i = indexOf; i< _participants.Count; i++)
			{
				_participants[i].Ordinal = indexOf;
			}
		}

		public void OrderParticipant()
		{
			_participants.Sort(_participantComparer);
		}

		#endregion
	}

	class OrdinalComparer : IComparer<EncounterParticipant>
	{
		public int Compare(EncounterParticipant? x, EncounterParticipant? y)
		{
			if (x == null && y == null)
				return 0;
			else if (x == null)
				return -1;
			else if (y == null)
				return 1;

			return x.Ordinal.CompareTo(y.Ordinal);
		}
	}
}
