using AutoMapper;
using Tavernkeep.Domain.Contracts.Creatures;
using Tavernkeep.Domain.Entities.Library.Creatures;

namespace Tavernkeep.Application.Mapping.Profiles
{
	public class CreatureProfile : Profile
	{
		public CreatureProfile()
		{
			CreateMap<Creature, CreatureDto>();
			CreateMap<Creature, CreatureFullDto>();
		}
	}
}
