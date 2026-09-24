using System;
using System.Threading;
using System.Threading.Tasks;
using Readora.Domain.Common;

namespace Readora.Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
	IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

	Task BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));

	Task CommitTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));

	Task RollbackTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
}
