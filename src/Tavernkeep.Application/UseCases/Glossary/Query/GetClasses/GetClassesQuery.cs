using MediatR;
using Tavernkeep.Core.Entities.Templates;

namespace Tavernkeep.Application.UseCases.Glossary.Query.GetClasses
{
	public class GetClassesQuery : IRequest<List<ClassTemplate>> { }
}
