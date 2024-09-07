using AutoMapper;
using Tavernkeep.Core.Contracts.Glossary.Dtos;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Application.Mapping.Profiles.Glossary
{
    /// <summary>
    /// Mapping profile for the <see cref="AncestryTemplate"/> class.
    /// </summary>
	public class AncestryTemplateProfile : Profile
    {
        public AncestryTemplateProfile()
        {
			CreateMap<AncestryTemplate, AncestryTemplateShortDto>();
		}
    }
}
