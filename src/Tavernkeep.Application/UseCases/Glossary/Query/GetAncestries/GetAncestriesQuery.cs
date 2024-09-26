using MediatR;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Application.UseCases.Glossary.Query.GetAncestries
{
	public class GetAncestriesQuery : IRequest<List<AncestryTemplate>> { }
}
