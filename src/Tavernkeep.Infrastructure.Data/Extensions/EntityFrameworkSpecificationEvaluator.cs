using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Specifications;

namespace Tavernkeep.Infrastructure.Data.Extensions
{
	public static class EntityFrameworkSpecificationEvaluator<TEntity> where TEntity : class
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
		{
			var query = inputQuery;

			if (specification.Criteria != null)
				query = query.Where(specification.Criteria);

			query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

			return query;
		}
	}
}
