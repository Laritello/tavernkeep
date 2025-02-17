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
				.Include<CharacterEncounterParticipant, CharacterEncounterParticipantDto>()
				.Include<CreatureEncounterParticipant, CreatureEncounterParticipantDto>();

			CreateMap<CharacterEncounterParticipant, CharacterEncounterParticipantDto>()
				.ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.Character.Id));

			CreateMap<CreatureEncounterParticipant, CreatureEncounterParticipantDto>()
				.ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.Creature.Id));
		}
	}
}
