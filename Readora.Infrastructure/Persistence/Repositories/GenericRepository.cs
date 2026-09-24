using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Common;

namespace Readora.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
	private readonly ApplicationDbContext _dbContext;

	public GenericRepository(ApplicationDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
	{
		return await ((DbContext)_dbContext).Set<T>().FindAsync(new object[1] { id }, cancellationToken);
	}

	public async Task<T?> GetEntityWithSpecAsync(ISpecification<T> spec, CancellationToken cancellationToken = default(CancellationToken))
	{
		return await EntityFrameworkQueryableExtensions.FirstOrDefaultAsync<T>(ApplySpecification(spec), cancellationToken);
	}

	public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
	{
		return await EntityFrameworkQueryableExtensions.ToListAsync<T>((IQueryable<T>)((DbContext)_dbContext).Set<T>(), cancellationToken);
	}

	public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec, CancellationToken cancellationToken = default(CancellationToken))
	{
		return await EntityFrameworkQueryableExtensions.ToListAsync<T>(ApplySpecification(spec), cancellationToken);
	}

	public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken = default(CancellationToken))
	{
		return await EntityFrameworkQueryableExtensions.CountAsync<T>(ApplySpecification(spec), cancellationToken);
	}

	public async Task<bool> AnyAsync(ISpecification<T> spec, CancellationToken cancellationToken = default(CancellationToken))
	{
		return await EntityFrameworkQueryableExtensions.AnyAsync<T>(ApplySpecification(spec), cancellationToken);
	}

	public async Task AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
	{
		await ((DbContext)_dbContext).Set<T>().AddAsync(entity, cancellationToken);
	}

	public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default(CancellationToken))
	{
		await ((DbContext)_dbContext).Set<T>().AddRangeAsync(entities, cancellationToken);
	}

	public void Update(T entity)
	{
		((DbContext)_dbContext).Set<T>().Attach(entity);
		((EntityEntry)((DbContext)_dbContext).Entry<T>(entity)).State = (EntityState)3;
	}

	public void Delete(T entity)
	{
		((DbContext)_dbContext).Set<T>().Remove(entity);
	}

	private IQueryable<T> ApplySpecification(ISpecification<T> spec)
	{
		return SpecificationEvaluator<T>.GetQuery(((DbContext)_dbContext).Set<T>().AsQueryable(), spec);
	}
}
