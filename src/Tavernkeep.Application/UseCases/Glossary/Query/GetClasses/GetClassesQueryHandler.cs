using MediatR;
using Tavernkeep.Core.Entities.Templates;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Glossary.Query.GetClasses
{
	public class GetClassesQueryHandler(IClassTemplateRepository classRepository) : IRequestHandler<GetClassesQuery, List<ClassTemplate>>
	{
		public async Task<List<ClassTemplate>> Handle(GetClassesQuery request, CancellationToken cancellationToken)
		{
			return await classRepository.GetAllClassesAsync(cancellationToken);
		}
	}
}
