using AutoMapper;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Application.Mapping.Profiles
{
	internal class ArmorProfile : Profile
	{
		public ArmorProfile()
		{
			CreateMap<Armor, ArmorDto>()
				.ForMember(
					dest => dest.Proficiencies,
					opt => opt.MapFrom(src =>
						new Dictionary<ArmorType, Proficiency>()
						{
							{ ArmorType.Unarmored, src.Proficiencies.Unarmored },
							{ ArmorType.Light, src.Proficiencies.Light },
							{ ArmorType.Medium, src.Proficiencies.Medium },
							{ ArmorType.Heavy, src.Proficiencies.Heavy },
						}));
		}
	}
}
