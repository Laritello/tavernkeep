using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Strategies.Encounters;

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
					var roll = diceService.Roll(bonus: creatureParticipant.Origin.Perception);
					participant.Initiative = roll.Value;
				}
				else
				{
					var bonus = creatureParticipant.Origin.GetSkillBonus(skillName);
					var roll = diceService.Roll(bonus: bonus);
					participant.Initiative = roll.Value;
				}
			}

			return Task.CompletedTask;
		}
	}
}
