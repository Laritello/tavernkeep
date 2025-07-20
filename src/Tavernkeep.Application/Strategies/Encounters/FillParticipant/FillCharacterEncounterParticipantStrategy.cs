using Tavernkeep.Application.Interfaces;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Encounters.Participants;
using Tavernkeep.Domain.Strategies.Encounters;

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
