using AutoMapper;
using Tavernkeep.Core.Contracts.Creatures;
using Tavernkeep.Core.Entities.Library.Creatures;

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
