using AutoMapper;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Entities.Pathfinder.Properties;

namespace Tavernkeep.Application.Mapping.Profiles
{
	/// <summary>
	/// Mapping profile for the <see cref="Character"/> class.
	/// </summary>
	public class CharacterProfile : Profile
	{
		public CharacterProfile()
		{
			CreateMap<Character, CharacterDto>()
				.ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Where(x => x.Type == SkillType.Basic || x.Type == SkillType.Lore || x.Type == SkillType.Custom)))
				.ForMember(dest => dest.Perception, opt => opt.MapFrom(src => src.Skills["Perception"]))
				.ForMember(
					dest => dest.Speeds,
					opt => opt.MapFrom(src =>
						new Dictionary<SpeedType, Speed>()
						{
							{ SpeedType.Walk, src.Walk },
							{ SpeedType.Burrow, src.Burrow },
							{ SpeedType.Climb, src.Climb },
							{ SpeedType.Fly, src.Fly },
							{ SpeedType.Swim, src.Swim },
						}));

			CreateMap<Ability, AbilityDto>();
			CreateMap<Skill, SkillDto>();
			CreateMap<Skill, SkillShortDto>();

			CreateMap<SavingThrow, SavingThrowDto>();
		}
	}
}
