﻿using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Extensions;

namespace Tavernkeep.Infrastructure.Data.Repositories.Base
{
	public class EntityFrameworkNameRepository<T> : IStringRepositoryBase<T, string> where T : StringEntity
	{
		protected readonly SessionContext Context;

		protected EntityFrameworkNameRepository(SessionContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		protected IQueryable<T> AsQueryable() => Context.Set<T>();

		public async Task<T?> FindAsync(string id, ISpecification<T> specification = default!, CancellationToken cancellationToken = default)
		{
			var query = AsQueryable();
			if (specification != null)
				query = EntityFrameworkSpecificationEvaluator<T>.GetQuery(query, specification);

			return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
		}

		public async Task<T?> FindAsync(ISpecification<T> specification = default!, CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(specification);

			var query = AsQueryable();
			if (specification != null)
				query = EntityFrameworkSpecificationEvaluator<T>.GetQuery(query, specification);

			return await query.FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<IList<T>> GetAsync(IEnumerable<string> ids, ISpecification<T> specification = default!, CancellationToken cancellationToken = default)
		{
			var query = AsQueryable().Where(x => ids.Any(z => x.Id == z));

			if (specification != null)
				query = EntityFrameworkSpecificationEvaluator<T>.GetQuery(query, specification);

			return await query.ToListAsync(cancellationToken);
		}

		public async Task<IList<T>> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
		{
			var query = EntityFrameworkSpecificationEvaluator<T>.GetQuery(AsQueryable(), specification);
			return await query.ToListAsync(cancellationToken);
		}

		public async Task<IList<T>> GetAsync(IEnumerable<string> ids, CancellationToken cancellationToken)
		{
			var query = AsQueryable().Where(x => ids.Any(z => x.Id == z));
			return await query.ToListAsync(cancellationToken);
		}

		public virtual void Save(T entity)
		{
			Context.Set<T>().Update(entity);
		}

		public void Save(IEnumerable<T> entities)
		{
			foreach (var entity in entities)
			{
				Save(entity);
			}
		}

		public virtual void Remove(T entity) => Context.Remove(entity);

		public void Remove(IEnumerable<T> entities) => Context.RemoveRange(entities);

		public async Task CommitAsync(CancellationToken cancellationToken = default) => await Context.SaveChangesAsync(cancellationToken);
	}
}
