using MediatR;
using Tavernkeep.Domain.Entities.Library.Conditions;
using Tavernkeep.Domain.Repositories;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions;

public class GetConditionsQueryHandler(
	IConditionLibraryRepository conditionRepository
	) : IRequestHandler<GetConditionsQuery, List<Condition>>
{
	public async Task<List<Condition>> Handle(GetConditionsQuery request, CancellationToken cancellationToken)
	{
		return await conditionRepository.GetAllConditionsAsync(cancellationToken);
	}
}
