using MediatR;
using Tavernkeep.Core.Entities.Pathfinder;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions
{
	public class GetConditionsQuery : IRequest<List<Condition>>
	{
	}
}
