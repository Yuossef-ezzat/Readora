using System;
using System.Linq.Expressions;
using Readora.Application.Specifications;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Authors.Queries;

public class AuthorsSpecification : BaseSpecification<Author>
{
	public AuthorsSpecification(string? searchTerm, int? page, int? pageSize)
		: base(string.IsNullOrEmpty(searchTerm) ? null : ((Expression<Func<Author, bool>>)((Author a) => a.Name.Contains(searchTerm))))
	{
		if (page.HasValue && pageSize.HasValue)
		{
			ApplyPaging((page.Value - 1) * pageSize.Value, pageSize.Value);
		}
	}
}
