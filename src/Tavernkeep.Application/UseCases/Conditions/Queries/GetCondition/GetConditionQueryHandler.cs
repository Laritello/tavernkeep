using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetCondition
{
	public class GetConditionQueryHandler(
		IConditionMetadataRepository conditionRepository
		) : IRequestHandler<GetConditionQuery, ConditionTemplate>
	{
		public async Task<ConditionTemplate> Handle(GetConditionQuery request, CancellationToken cancellationToken)
		{
			return await conditionRepository.GetConditionAsync(request.Name, cancellationToken);
		}
	}
}
