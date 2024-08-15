using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Specifications;

namespace Tavernkeep.Core.Repositories
{
	public interface IGuidRepositoryBase<T, in TId> where T : GuidEntity
	{
		Task<T?> FindAsync(TId id, ISpecification<T> specification = default!, CancellationToken cancellationToken = default);
		Task<T?> FindAsync(ISpecification<T> specification = default!, CancellationToken cancellationToken = default);

		Task<IList<T>> GetAsync(IEnumerable<TId> ids, ISpecification<T> specification = default!, CancellationToken cancellationToken = default);
		Task<IList<T>> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
		void Save(T entity);
		void Save(IEnumerable<T> entities);

		void Remove(T entity);
		void Remove(IEnumerable<T> entities);

		Task CommitAsync(CancellationToken cancellationToken = default);
	}
}
