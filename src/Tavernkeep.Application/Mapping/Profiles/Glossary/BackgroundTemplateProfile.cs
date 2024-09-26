using AutoMapper;
using Tavernkeep.Core.Contracts.Glossary.Dtos;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Application.Mapping.Profiles.Glossary
{
	/// <summary>
	/// Mapping profile for the <see cref="BackgroundTemplate"/> class.
	/// </summary>
	public class BackgroundTemplateProfile : Profile
    {
        public BackgroundTemplateProfile() 
        {
			CreateMap<BackgroundTemplate, BackgroundTemplateShortDto>();
		}
    }
}
