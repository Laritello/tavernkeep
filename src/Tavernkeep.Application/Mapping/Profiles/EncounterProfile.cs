using AutoMapper;
using Tavernkeep.Domain.Contracts.Character.Dtos;
using Tavernkeep.Domain.Contracts.Conditions.Dtos;
using Tavernkeep.Domain.Contracts.Encounters.Dtos;
using Tavernkeep.Domain.Contracts.Enums;
using Tavernkeep.Domain.Entities.Encounters;
using Tavernkeep.Domain.Entities.Encounters.Participants;
using Tavernkeep.Domain.Entities.Pathfinder.Properties;

namespace Tavernkeep.Application.Mapping.Profiles;

public class EncounterProfile : Profile
{
	private readonly Func<Skill, bool> savingThrows = x => x.Type == SkillType.SavingThrow;

	public EncounterProfile()
	{
		CreateMap<Encounter, EncounterDto>()
			.ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Created.ToUnixTimeSeconds()))
			.ForMember(dest => dest.Participants, opt => opt.MapFrom(src => src.Participants.OrderBy(x => x.Ordinal)));

		CreateMap<EncounterParticipant, EncounterParticipantDto>()
			.Include<CharacterEncounterParticipant, CharacterEncounterParticipantDto>()
			.Include<CreatureEncounterParticipant, CreatureEncounterParticipantDto>();

		CreateMap<CharacterEncounterParticipant, CharacterEncounterParticipantDto>()
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Character.Name))
			.ForMember(dest => dest.Perception, opt => opt.MapFrom(src => src.Character.Skills["Perception"].Bonus))
			.ForMember(dest => dest.ArmorClass, opt => opt.MapFrom(src => src.Character.Armor.Class))
			.ForMember(dest => dest.SavingThrows, opt => opt.MapFrom(src => src.Character.Skills.Where(savingThrows).ToDictionary(x => x.Name, x => x.Bonus)))
			.ForMember(dest => dest.Health, opt => opt.MapFrom(src => src.Character.Health))
			.ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.Character.Id))
			.ForMember(dest => dest.Conditions, opt => opt.MapFrom(src => src.Character.Conditions.Select(x =>
				new ConditionShortDto()
				{
					Name = x.Condition.Name,
					HasLevels = x.Condition.HasLevels,
					Level = x.Level
				}
			)));

		CreateMap<CreatureEncounterParticipant, CreatureEncounterParticipantDto>()
			.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Creature.Name))
			.ForMember(dest => dest.Perception, opt => opt.MapFrom(src => src.Creature.Perception))
			.ForMember(dest => dest.ArmorClass, opt => opt.MapFrom(src => src.Creature.ArmorClass))
			.ForMember(dest => dest.SavingThrows, opt => opt.MapFrom(src => src.Creature.SavingThrows))
			.ForMember(dest => dest.Health, opt => opt.MapFrom(src => new HealthDto() { Max = 10, Current = 10, Temporary = 0 }))
			.ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.Creature.Id))
			.ForMember(dest => dest.Statblock, opt => opt.MapFrom(src => src.Creature.Statblock))
			.ForMember(dest => dest.Conditions, opt => opt.MapFrom(src => src.Conditions.Select(x =>
				new ConditionShortDto()
				{
					Name = x.Condition.Name,
					HasLevels = x.Condition.HasLevels,
					Level = x.Level
				}
			)));
	}
}
