using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetCondition
{
	public class GetConditionQuery(string name) : IRequest<ConditionMetadata>
	{
		public string Name { get; set; } = name;
	}
}
