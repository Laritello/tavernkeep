using Microsoft.EntityFrameworkCore;
using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Repositories;
using Tavernkeep.Core.Specifications;
using Tavernkeep.Infrastructure.Data.Context;
using Tavernkeep.Infrastructure.Data.Extensions;

namespace Tavernkeep.Infrastructure.Data.Repositories
{
    public class EntityFrameworkRepository<T> : IRepositoryBase<T, Guid> where T : Entity
    {
        protected readonly SessionContext Context;

        protected EntityFrameworkRepository(SessionContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected IQueryable<T> AsQueryable() => Context.Set<T>();

        public async Task<T?> FindAsync(Guid id, ISpecification<T> specification = default!)
        {
            var query = AsQueryable();
            if (specification != null)
                query = EntityFrameworkSpecificationEvaluator<T>.GetQuery(query, specification);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> FindAsync(ISpecification<T> specification = default!)
        {
            ArgumentNullException.ThrowIfNull(specification);

            var query = AsQueryable();
            if (specification != null)
                query = EntityFrameworkSpecificationEvaluator<T>.GetQuery(query, specification);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IList<T>> GetAsync(IEnumerable<Guid> ids, ISpecification<T> specification = default!)
        {
            var query = AsQueryable().Where(x => ids.Any(z => x.Id == z));

            if (specification != null)
                query = EntityFrameworkSpecificationEvaluator<T>.GetQuery(query, specification);

            return await query.ToListAsync();
        }

        public async Task<IList<T>> GetAsync(ISpecification<T> specification)
        {
            var query = EntityFrameworkSpecificationEvaluator<T>.GetQuery(AsQueryable(), specification);
            return await query.ToListAsync();
        }

        public async Task<IList<T>> GetAsync(IEnumerable<Guid> ids)
        {
            var query = AsQueryable().Where(x => ids.Any(z => x.Id == z));
            return await query.ToListAsync();
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

        public async Task CommitAsync(CancellationToken ct = default) => await Context.SaveChangesAsync(ct);
    }
}
