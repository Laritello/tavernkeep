using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetCondition
{
	public class GetConditionQueryHandler(
		IConditionLibraryRepository conditionRepository
		) : IRequestHandler<GetConditionQuery, Condition>
	{
		public async Task<Condition> Handle(GetConditionQuery request, CancellationToken cancellationToken)
		{
			return await conditionRepository.GetConditionAsync(request.Name, cancellationToken);
		}
	}
}
