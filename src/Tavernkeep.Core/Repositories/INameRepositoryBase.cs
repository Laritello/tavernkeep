using Tavernkeep.Core.Entities.Base;
using Tavernkeep.Core.Specifications;

namespace Tavernkeep.Core.Repositories
{
	public interface INameRepositoryBase<T, in TName> where T : NameEntity
	{
		Task<T?> FindAsync(TName name, ISpecification<T> specification = default!, CancellationToken cancellationToken = default);
		Task<T?> FindAsync(ISpecification<T> specification = default!, CancellationToken cancellationToken = default);

		Task<IList<T>> GetAsync(IEnumerable<TName> names, ISpecification<T> specification = default!, CancellationToken cancellationToken = default);
		Task<IList<T>> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
		void Save(T entity);
		void Save(IEnumerable<T> entities);

		void Remove(T entity);
		void Remove(IEnumerable<T> entities);

		Task CommitAsync(CancellationToken cancellationToken = default);
	}
}
