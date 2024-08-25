using MediatR;
using Tavernkeep.Core.Entities.Pathfinder.Classes;

namespace Tavernkeep.Application.UseCases.Glossary.Query.GetClasses
{
	public class GetClassesQuery : IRequest<List<ClassMetadata>> { }
}
