using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Readora.Application.Interfaces.Persistence;
using Readora.Domain.Common;

namespace Readora.Infrastructure.Persistence.Repositories;

public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
{
	public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
	{
		IQueryable<TEntity> query = inputQuery;
		if (spec.Criteria != null)
		{
			query = query.Where(spec.Criteria);
		}
		foreach (Expression<Func<TEntity, object>> include in spec.Includes)
		{
			query = (IQueryable<TEntity>)EntityFrameworkQueryableExtensions.Include<TEntity, object>(query, include);
		}
		if (spec.OrderBy != null)
		{
			query = query.OrderBy(spec.OrderBy);
		}
		else if (spec.OrderByDescending != null)
		{
			query = query.OrderByDescending(spec.OrderByDescending);
		}
		if (spec.IsPagingEnabled)
		{
			query = query.Skip(spec.Skip).Take(spec.Take);
		}
		return query;
	}
}
