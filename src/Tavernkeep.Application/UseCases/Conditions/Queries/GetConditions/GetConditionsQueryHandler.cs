using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions
{
	public class GetConditionsQueryHandler(
		IConditionMetadataRepository conditionRepository
		) : IRequestHandler<GetConditionsQuery, List<ConditionTemplate>>
	{
		public async Task<List<ConditionTemplate>> Handle(GetConditionsQuery request, CancellationToken cancellationToken)
		{
			return await conditionRepository.GetAllConditionsAsync(cancellationToken);
		}
	}
}
