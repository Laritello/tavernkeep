using AutoMapper;
using Tavernkeep.Core.Contracts.Encounters.Dtos;
using Tavernkeep.Core.Entities.Encounters;

namespace Tavernkeep.Application.Mapping.Profiles
{
	public class EncounterProfile : Profile
	{
		public EncounterProfile() 
		{
			CreateMap<Encounter, EncounterDto>();
		}
	}
}
