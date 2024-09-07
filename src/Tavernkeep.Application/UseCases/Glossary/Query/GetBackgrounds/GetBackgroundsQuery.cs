using MediatR;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Application.UseCases.Glossary.Query.GetBackgrounds
{
	public class GetBackgroundsQuery : IRequest<List<BackgroundTemplate>>;
}
