using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tavernkeep.Application.Interfaces;
using Tavernkeep.Application.Services;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Exceptions;
using Tavernkeep.Core.Services.Encounters.Strategies;

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
