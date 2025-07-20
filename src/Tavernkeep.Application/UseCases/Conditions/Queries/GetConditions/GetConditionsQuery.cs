using MediatR;
using Tavernkeep.Domain.Entities.Library.Conditions;

namespace Tavernkeep.Application.UseCases.Conditions.Queries.GetConditions;

public class GetConditionsQuery : IRequest<List<Condition>>
{
}
