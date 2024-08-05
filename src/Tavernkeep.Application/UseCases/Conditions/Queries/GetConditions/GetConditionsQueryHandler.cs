using MediatR;
using Tavernkeep.Core.Entities.Conditions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions
{
	public class GetConditionsQueryHandler(
		IConditionMetadataRepository conditionRepository
		) : IRequestHandler<GetConditionsQuery, List<ConditionMetadata>>
	{
		public async Task<List<ConditionMetadata>> Handle(GetConditionsQuery request, CancellationToken cancellationToken)
		{
			return await conditionRepository.GetAllConditionsAsync(cancellationToken);
		}
	}
}
