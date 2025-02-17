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
		// Bake in expressions for mapping?
		private readonly Func<Skill, bool> skillsAreSkills = x => x.Type == SkillType.Basic || x.Type == SkillType.Lore || x.Type == SkillType.Custom;
		private readonly Func<Skill, bool> skillsAreSavingThrows = x => x.Type == SkillType.SavingThrow;
		private readonly List<string> abilitiesOrder = ["Strength", "Dexterity", "Intelligence", "Constitution", "Wisdom", "Charisma"];

		public CharacterProfile()
		{
			CreateMap<Character, CharacterDto>()
				.ForMember(dest => dest.Abilities, opt => opt.MapFrom(src => src.Abilities.OrderBy(x => abilitiesOrder.IndexOf(x.Name))))
				.ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Where(skillsAreSkills).OrderByDescending(x => x.Pinned).ThenBy(x => x.Type).ThenBy(x => x.Name)))
				.ForMember(dest => dest.SavingThrows, opt => opt.MapFrom(src => src.Skills.Where(skillsAreSavingThrows)))
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

			CreateMap<Character, CharacterTemplateDto>()
				.ForMember(dest => dest.Skills, opt => opt.MapFrom(src => src.Skills.Where(skillsAreSkills).OrderBy(x => x.Pinned).ThenBy(x => x.Type).ThenBy(x => x.Name)))
				.ForMember(dest => dest.SavingThrows, opt => opt.MapFrom(src => src.Skills.Where(skillsAreSavingThrows)))
				.ForMember(dest => dest.Perception, opt => opt.MapFrom(src => src.Skills["Perception"]));

			CreateMap<Character, CharacterEncounterDto>()
				.ForMember(dest => dest.SavingThrows, opt => opt.MapFrom(src => src.Skills.Where(skillsAreSavingThrows)))
				.ForMember(dest => dest.Perception, opt => opt.MapFrom(src => src.Skills["Perception"]));

			CreateMap<Ability, AbilityDto>();

			CreateMap<Skill, SkillDto>();
			CreateMap<Skill, SkillShortDto>();
			CreateMap<Skill, SavingThrowDto>();

			CreateMap<Health, HealthDto>();

			CreateMap<Ancestry, AncestryDto>();
			CreateMap<Class, ClassDto>();
		}
	}
}
