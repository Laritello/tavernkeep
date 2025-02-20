using AutoMapper;
using Tavernkeep.Core.Contracts.Conditions.Dtos;
using Tavernkeep.Core.Entities.Pathfinder;

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
		}
	}
}
