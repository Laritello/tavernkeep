using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Services.Encounters.Strategies;

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
					var roll = diceService.Roll(bonus: creatureParticipant.Creature.Perception);
					participant.Initiative = roll.Value;
				}
				else if (creatureParticipant.Creature.Skills.TryGetValue(skillName, out var bonus))
				{
					var roll = diceService.Roll(bonus: bonus);
					participant.Initiative = roll.Value;
				}
				else
				{
					throw new BusinessLogicException("Creature doesn't have specified skill");
				}
			}

			return Task.CompletedTask;
		}
	}
}
