using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions
{
	public class GetConditionsQueryHandler(
		IConditionLibraryRepository conditionRepository
		) : IRequestHandler<GetConditionsQuery, List<ConditionInformation>>
	{
		public async Task<List<ConditionInformation>> Handle(GetConditionsQuery request, CancellationToken cancellationToken)
		{
			return await conditionRepository.GetAllConditionsAsync(cancellationToken);
		}
	}
}
