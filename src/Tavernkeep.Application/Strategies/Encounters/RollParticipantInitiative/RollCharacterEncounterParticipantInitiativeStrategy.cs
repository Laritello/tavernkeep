using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.RollParticipantInitiative
{
	public class RollCharacterEncounterParticipantInitiativeStrategy(IDiceService diceService) : IRollEncounterParticipantInitiativeStrategy
	{
		public EncounterParticipantType Type => EncounterParticipantType.Character;

		public Task RollInitiative(EncounterParticipant participant, CancellationToken cancellationToken, string skillName = "Perception")
		{
			if (participant is CharacterEncounterParticipant characterParticipant)
			{
				var skill = characterParticipant.Character.Skills[skillName] ?? throw new BusinessLogicException("Character doesn't have specified skill");
				var roll = diceService.Roll(bonus: skill.Bonus);

				participant.Initiative = roll.Value;
			}

			return Task.CompletedTask;
		}
	}
}
