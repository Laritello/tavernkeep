using AutoMapper;
using Tavernkeep.Core.Contracts.Encounters.Dtos;
using Tavernkeep.Core.Entities.Encounters;
using Tavernkeep.Core.Entities.Encounters.Participants;

namespace Tavernkeep.Application.Mapping.Profiles
{
	public class EncounterProfile : Profile
	{
		public EncounterProfile() 
		{
			CreateMap<Encounter, EncounterDto>();

			CreateMap<EncounterParticipant, EncounterParticipantDto>()
				.Include<CharacterEncounterParticipant, CharacterEncounterParticipantDto>();

			CreateMap<CharacterEncounterParticipant, CharacterEncounterParticipantDto>();
		}
	}
}
