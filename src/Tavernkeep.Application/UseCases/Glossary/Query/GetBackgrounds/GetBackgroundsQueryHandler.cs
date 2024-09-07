using MediatR;
using Tavernkeep.Core.Entities.Templates;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Glossary.Query.GetBackgrounds
{
	public class GetBackgroundsQueryHandler(IBackgroundTemplateRepository backgroundRepository) : IRequestHandler<GetBackgroundsQuery, List<BackgroundTemplate>>
	{
		public async Task<List<BackgroundTemplate>> Handle(GetBackgroundsQuery request, CancellationToken cancellationToken)
		{
			return await backgroundRepository.GetAllBackgroundsAsync(cancellationToken);
		}
	}
}
