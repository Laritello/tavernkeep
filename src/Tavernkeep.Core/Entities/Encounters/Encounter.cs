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
			RoundNumber = 1;
		}

		#endregion

		#region Properties

		public string Name { get; set; }
		public int RoundNumber { get; private set; }
		public int CurrentTurnIndex { get; private set; }
		public EncounterStatus Status { get; set; }
		public IReadOnlyCollection<EncounterParticipant> Participants => _participants.AsReadOnly();

		#endregion

		#region Methods

		/// <summary>
		/// Add participant to the encounter.
		/// </summary>
		/// <param name="participant">The participant that should be added to the encounter.</param>
		public void AddParticipant(EncounterParticipant participant)
		{
			participant.Ordinal = _participants.Count;
			_participants.Add(participant);
		}

		/// <summary>
		/// Remove participant from the encounter.
		/// </summary>
		/// <param name="participant">The participant that should be removed from the encounter.</param>
		public void RemoveParticipant(EncounterParticipant participant)
		{
			int indexOf = _participants.IndexOf(participant);
			_participants.Remove(participant);

			// Update ordinal for all the participants after the deleted one
			for (int i = indexOf; i< _participants.Count; i++)
			{
				_participants[i].Ordinal = indexOf;
			}

			/*
			 * If we deleted the participant when our current turn pointer was on the last participant
			 * move the pointer to the updated last index.
			 */
			if (CurrentTurnIndex >= _participants.Count)
			{
				CurrentTurnIndex = _participants.Count - 1;
			}
		}

		public void OrderByInitiative()
		{
			int ordinal = 0;

			foreach (var participant in _participants.OrderByDescending(x => x.Initiative))
			{
				participant.Ordinal = ordinal++;
			}
		}

		public void NextTurn()
		{
			// There is no next turn if we have no participants
			if (Participants.Count == 0)
			{
				return;
			}

			/*
			 * If we overflow, we should set index to the first participant
			 * And increase round number by 1.
			 */
			if (CurrentTurnIndex >= Participants.Count - 1)
			{
				CurrentTurnIndex = 0;
				RoundNumber++;
			}
			else
			{
				CurrentTurnIndex++;
			}
		}

		public void PreviousTurn()
		{
			// If we have no participants or hit start of combat - there is no previous turn.
			if (Participants.Count == 0 || (RoundNumber == 1 && CurrentTurnIndex == 0))
			{
				return;
			}

			/*
			 * If we underflow, we should set index to the last participant
			 * And decrease round number by 1.
			 */
			if (CurrentTurnIndex <= 0)
			{
				CurrentTurnIndex = Participants.Count - 1;
				RoundNumber = Math.Max(1, RoundNumber - 1);
			}
			else
			{
				CurrentTurnIndex--;
			}
		}

		#endregion
	}
}
