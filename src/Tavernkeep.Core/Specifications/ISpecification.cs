using System.Linq.Expressions;

namespace Tavernkeep.Core.Specifications
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }

        bool IsSatisfiedBy(T @object);
    }
}
