using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Readora.Domain.Common;

namespace Readora.Application.Interfaces.Persistence;

public interface IGenericRepository<T> where T : BaseEntity
{
	Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

	Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default(CancellationToken));

	Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

	Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default(CancellationToken));

	Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default(CancellationToken));

	Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken cancellationToken = default(CancellationToken));

	Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

	Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken));

	void Update(T entity);

	void Delete(T entity);
}
