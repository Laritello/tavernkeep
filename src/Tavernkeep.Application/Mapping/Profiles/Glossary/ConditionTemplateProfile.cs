﻿using AutoMapper;
using Tavernkeep.Core.Contracts.Conditions.Dtos;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Application.Mapping.Profiles.Glossary
{
	/// <summary>
	/// Mapping profile for the <see cref="ConditionInformation"/> class.
	/// </summary>
	public class ConditionTemplateProfile : Profile
	{
		public ConditionTemplateProfile()
		{
			CreateMap<ConditionInformation, ConditionTemplateDto>();
		}
	}
}
