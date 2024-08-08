using AutoMapper;
using Tavernkeep.Core.Contracts.Conditions.Dtos;
using Tavernkeep.Core.Entities.Conditions;

namespace Tavernkeep.Application.Mapping.Profiles
{
	public class ConditionProfile : Profile
	{
		public ConditionProfile()
		{
			CreateMap<ConditionMetadata, ConditionMetadataDto>();
		}
	}
}
