using AutoMapper;
using Tavernkeep.Core.Contracts.Conditions.Dtos;
using Tavernkeep.Core.Entities.Library.Conditions;

namespace Tavernkeep.Application.Mapping.Profiles.Glossary
{
	/// <summary>
	/// Mapping profile for the <see cref="Condition"/> class.
	/// </summary>
	public class ConditionProfile : Profile
	{
		public ConditionProfile()
		{
			CreateMap<Condition, ConditionDto>();

			CreateMap<ConditionRelated, ConditionDto>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Condition.Name))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Condition.Description))
				.ForMember(dest => dest.HasLevels, opt => opt.MapFrom(src => src.Condition.HasLevels))
				.ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.Level));
		}
	}
}
