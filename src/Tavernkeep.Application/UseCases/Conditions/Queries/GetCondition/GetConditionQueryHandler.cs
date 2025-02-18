using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetCondition
{
	public class GetConditionQueryHandler(
		IConditionLibraryRepository conditionRepository
		) : IRequestHandler<GetConditionQuery, ConditionInformation>
	{
		public async Task<ConditionInformation> Handle(GetConditionQuery request, CancellationToken cancellationToken)
		{
			return await conditionRepository.GetConditionAsync(request.Name, cancellationToken);
		}
	}
}
