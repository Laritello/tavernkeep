using Tavernkeep.Application.Interfaces;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Encounters.Participants;
using Tavernkeep.Domain.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.RollParticipantInitiative
{
	public class RollCreatureEncounterParticipantInitiativeStrategy(IDiceService diceService) : IRollEncounterParticipantInitiativeStrategy
	{
		public EncounterParticipantType Type => EncounterParticipantType.Creature;

		public Task RollInitiative(EncounterParticipant participant, CancellationToken cancellationToken, string skillName = "Perception")
		{
			if (participant is CreatureEncounterParticipant creatureParticipant)
			{
				if (skillName is "Perception")
				{
					var roll = diceService.Roll(bonus: 0);
					participant.Initiative = roll.Value;
				}
				else
				{
					var bonus = 0;
					var roll = diceService.Roll(bonus: bonus);
					participant.Initiative = roll.Value;
				}
			}

			return Task.CompletedTask;
		}
	}
}
