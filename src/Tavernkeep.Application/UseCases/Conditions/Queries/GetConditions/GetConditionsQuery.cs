using MediatR;
using Tavernkeep.Core.Entities.Conditions;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions
{
	public class GetConditionsQuery : IRequest<List<ConditionMetadata>>
	{
	}
}
