using Tavernkeep.Application.Interfaces;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Encounters;
using Tavernkeep.Domain.Entities.Encounters.Participants;
using Tavernkeep.Domain.Exceptions;
using Tavernkeep.Domain.Strategies.Encounters;

namespace Tavernkeep.Application.Strategies.Encounters.AddParticipant
{
	public class AddCharacterEncounterParticipantStrategy(ICharacterService characterService) : IAddEncounterParticipantStrategy
	{
		public EncounterParticipantType Type => EncounterParticipantType.Character;
		public async Task AddParticipantAsync(Encounter encounter, Guid entityId, CancellationToken cancellationToken)
		{
			var character = await characterService.GetCharacterAsync(entityId, cancellationToken)
				?? throw new BusinessLogicException("Character with specified ID not found");

			CharacterEncounterParticipant participant = new()
			{
				Encounter = encounter,
				Character = character,
			};

			encounter.AddParticipant(participant);
		}
	}
}
