using MediatR;
using Tavernkeep.Core.Entities.Library.Conditions;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetCondition
{
	public class GetConditionQuery(string name) : IRequest<Condition>
	{
		public string Name { get; set; } = name;
	}
}
