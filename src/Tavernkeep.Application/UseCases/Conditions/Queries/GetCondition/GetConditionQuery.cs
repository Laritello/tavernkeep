using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Conditions;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetCondition
{
	public class GetConditionQuery(string name) : IRequest<ConditionInformation>
	{
		public string Name { get; set; } = name;
	}
}
