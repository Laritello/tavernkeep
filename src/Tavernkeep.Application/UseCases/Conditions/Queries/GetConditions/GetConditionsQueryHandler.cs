using MediatR;
using Tavernkeep.Core.Entities.Library.Conditions;
using Tavernkeep.Core.Repositories;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions
{
	public class GetConditionsQueryHandler(
		IConditionLibraryRepository conditionRepository
		) : IRequestHandler<GetConditionsQuery, List<Condition>>
	{
		public async Task<List<Condition>> Handle(GetConditionsQuery request, CancellationToken cancellationToken)
		{
			return await conditionRepository.GetAllConditionsAsync(cancellationToken);
		}
	}
}
