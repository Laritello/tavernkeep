using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions
{
	public class GetConditionsQuery : IRequest<List<ConditionInformation>>
	{
	}
}
