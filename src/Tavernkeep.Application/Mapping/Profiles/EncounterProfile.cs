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
			CreateMap<Encounter, EncounterDto>()
				.ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants.OrderBy(x => x.Ordinal)));

			CreateMap<EncounterParticipant, EncounterParticipantDto>()
				.Include<CharacterEncounterParticipant, EncounterParticipantDto>();

			CreateMap<CharacterEncounterParticipant, EncounterParticipantDto>()
				.ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.Character.Id));
		}
	}
}
