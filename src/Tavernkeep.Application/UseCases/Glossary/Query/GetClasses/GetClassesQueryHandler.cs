using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Classes;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Glossary.Query.GetClasses
{
	public class GetClassesQueryHandler(IClassMetadataRepository classRepository) : IRequestHandler<GetClassesQuery, List<ClassMetadata>>
	{
		public async Task<List<ClassMetadata>> Handle(GetClassesQuery request, CancellationToken cancellationToken)
		{
			return await classRepository.GetAllClassesAsync(cancellationToken);
		}
	}
}
