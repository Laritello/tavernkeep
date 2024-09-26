using AutoMapper;
using Tavernkeep.Core.Contracts.Glossary.Dtos;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Application.Mapping.Profiles.Glossary
{
	/// <summary>
	/// Mapping profile for the <see cref="ClassTemplate"/> class.
	/// </summary>
	public class ClassTemplateProfile : Profile
	{
		public ClassTemplateProfile() 
		{
			CreateMap<ClassTemplate, ClassTemplateShortDto>();
		}
	}
}
