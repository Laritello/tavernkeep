using Tavernkeep.Application.Interfaces;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Encounters.Participants;
using Tavernkeep.Core.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.FillParticipant
{
	public class FillCharacterEncounterParticipantStrategy(ICharacterService characterService) : IFillEncounterParticipantStrategy
	{
		public EncounterParticipantType Type => EncounterParticipantType.Character;

		public async Task FillParticipantAsync(EncounterParticipant participant, CancellationToken cancellationToken)
		{
			if (participant is CharacterEncounterParticipant characterParticipant)
			{
				characterParticipant.Character = await characterService.GetCharacterAsync(characterParticipant.CharacterId, cancellationToken);
			}
		}
	}
}
