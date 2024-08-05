using MediatR;
using Tavernkeep.Core.Entities.Conditions;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetCondition
{
	public class GetConditionQuery : IRequest<ConditionMetadata>
	{
		public string Name { get; set; }

		public GetConditionQuery(string name)
		{
			Name = name;
		}
	}
}
