using System;
using System.Linq.Expressions;
using Readora.Application.Specifications;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Categories.Queries;

public class CategoriesSpecification : BaseSpecification<Category>
{
	public CategoriesSpecification(string? searchTerm, int? page, int? pageSize)
		: base(string.IsNullOrEmpty(searchTerm) ? null : ((Expression<Func<Category, bool>>)((Category c) => c.Name.Contains(searchTerm))))
	{
		if (page.HasValue && pageSize.HasValue)
		{
			ApplyPaging((page.Value - 1) * pageSize.Value, pageSize.Value);
		}
	}
}
