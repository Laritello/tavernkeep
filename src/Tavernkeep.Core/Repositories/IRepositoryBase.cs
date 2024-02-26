using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Specifications;

namespace Tavernkeep.Core.Repositories
{
    public interface IRepositoryBase<T, in TId> where T : Entity
    {
        Task<T?> FindAsync(TId id, ISpecification<T> specification = default!);
        Task<T?> FindAsync(ISpecification<T> specification = default!);

        Task<IList<T>> GetAsync(IEnumerable<TId> ids, ISpecification<T> specification = default!);
        Task<IList<T>> GetAsync(ISpecification<T> specification);
        void Save(T entity);
        void Save(IEnumerable<T> entities);

        void Remove(T entity);
        void Remove(IEnumerable<T> entities);

        Task CommitAsync(CancellationToken ct = default);
    }
}
