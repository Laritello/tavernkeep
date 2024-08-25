using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Ancestries;

namespace Tavernkeep.Application.UseCases.Glossary.Query.GetAncestries
{
	public class GetAncestriesQuery : IRequest<List<AncestryMetadata>> { }
}
