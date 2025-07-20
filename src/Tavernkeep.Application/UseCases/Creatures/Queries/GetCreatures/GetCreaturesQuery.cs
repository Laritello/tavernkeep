using MediatR;
using Tavernkeep.Domain.Entities.Library.Creatures;

namespace Tavernkeep.Application.UseCases.Creatures.Queries.GetCreatures;

public class GetCreaturesQuery : IRequest<ICollection<Creature>> { }
