using MediatR;
using Tavernkeep.Domain.Entities.Library.Conditions;
using Tavernkeep.Domain.Repositories;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetCondition;

public class GetConditionQueryHandler(
	IConditionLibraryRepository conditionRepository
	) : IRequestHandler<GetConditionQuery, Condition>
{
	public async Task<Condition> Handle(GetConditionQuery request, CancellationToken cancellationToken)
	{
		return await conditionRepository.GetConditionAsync(request.Name, cancellationToken);
	}
}
