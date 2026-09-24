using System;
using System.Linq.Expressions;
using Readora.Application.Specifications;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Books.Queries;

public class BooksSpecification : BaseSpecification<Book>
{
	public BooksSpecification(string? searchTerm, int? page, int? pageSize)
		: base(string.IsNullOrEmpty(searchTerm) ? null : ((Expression<Func<Book, bool>>)((Book b) => b.Title.Contains(searchTerm) || b.ISBN.Contains(searchTerm))))
	{
		if (page.HasValue && pageSize.HasValue)
		{
			ApplyPaging((page.Value - 1) * pageSize.Value, pageSize.Value);
		}
	}
}
