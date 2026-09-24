using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Readora.Application.Common.Exceptions;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Common;
using Readora.Infrastructure.Persistence.Repositories;

namespace Readora.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly ApplicationDbContext _dbContext;

	private Hashtable? _repositories;

	private IDbContextTransaction? _currentTransaction;

	public UnitOfWork(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
	{
		if (_repositories == null)
		{
			_repositories = new Hashtable();
		}
		string type = typeof(TEntity).Name;
		if (!_repositories.ContainsKey(type))
		{
			Type repositoryType = typeof(GenericRepository<>);
			object repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
			_repositories.Add(type, repositoryInstance);
		}
		return (IGenericRepository<TEntity>)_repositories[type];
	}

	public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		try
		{
			return await ((DbContext)_dbContext).SaveChangesAsync(cancellationToken);
		}
		catch (DbUpdateConcurrencyException ex)
		{
			DbUpdateConcurrencyException ex2 = ex;
			DbUpdateConcurrencyException ex3 = ex2;
			throw new ConcurrencyException("A concurrency error occurred.", (Exception)(object)ex3);
		}
	}

	public async Task BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		if (_currentTransaction == null)
		{
			_currentTransaction = await ((DbContext)_dbContext).Database.BeginTransactionAsync(cancellationToken);
		}
	}

	public async Task CommitTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		try
		{
			await SaveChangesAsync(cancellationToken);
			if (_currentTransaction != null)
			{
				await _currentTransaction.CommitAsync(cancellationToken);
			}
		}
		catch
		{
			await RollbackTransactionAsync(cancellationToken);
			throw;
		}
		finally
		{
			if (_currentTransaction != null)
			{
				((IDisposable)_currentTransaction).Dispose();
				_currentTransaction = null;
			}
		}
	}

	public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		try
		{
			if (_currentTransaction != null)
			{
				await _currentTransaction.RollbackAsync(cancellationToken);
			}
		}
		finally
		{
			if (_currentTransaction != null)
			{
				((IDisposable)_currentTransaction).Dispose();
				_currentTransaction = null;
			}
		}
	}

	public void Dispose()
	{
		((DbContext)_dbContext).Dispose();
		((IDisposable)_currentTransaction)?.Dispose();
	}
}
