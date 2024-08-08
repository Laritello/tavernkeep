using AutoMapper;
using Tavernkeep.Core.Contracts.Character.Dtos;
using Tavernkeep.Core.Contracts.Enums;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.Mapping.Profiles
{
	public class CharacterProfile : Profile
	{
		public CharacterProfile()
		{
			CreateMap<Character, CharacterDto>()
				.ForMember(
					dest => dest.Abilities,
					opt => opt.MapFrom(src =>
						new Dictionary<AbilityType, Ability>()
						{
							{ AbilityType.Strength, src.Strength },
							{ AbilityType.Dexterity, src.Dexterity },
							{ AbilityType.Constitution, src.Constitution },
							{ AbilityType.Intelligence, src.Intelligence },
							{ AbilityType.Wisdom, src.Wisdom },
							{ AbilityType.Charisma, src.Charisma },
						}
					))
				.ForMember(
					dest => dest.Skills,
					opt => opt.MapFrom(src =>
						new Dictionary<SkillType, Skill>()
						{
							{ SkillType.Acrobatics, src.Acrobatics },
							{ SkillType.Arcana, src.Arcana },
							{ SkillType.Athletics, src.Athletics },
							{ SkillType.Crafting, src.Crafting },
							{ SkillType.Deception, src.Deception },
							{ SkillType.Diplomacy, src.Diplomacy },
							{ SkillType.Intimidation, src.Intimidation },
							{ SkillType.Medicine, src.Medicine },
							{ SkillType.Nature, src.Nature },
							{ SkillType.Occultism, src.Occultism },
							{ SkillType.Performance, src.Performance },
							{ SkillType.Religion, src.Religion },
							{ SkillType.Society, src.Society },
							{ SkillType.Stealth, src.Stealth },
							{ SkillType.Survival, src.Survival },
							{ SkillType.Thievery, src.Thievery }
						}));
		}
	}
}
