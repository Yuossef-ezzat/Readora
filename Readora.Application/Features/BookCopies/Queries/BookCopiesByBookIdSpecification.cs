using System;
using System.Linq.Expressions;
using Readora.Application.Specifications;
using Readora.Domain.Entities;

namespace Readora.Application.Features.BookCopies.Queries;

public class BookCopiesByBookIdSpecification : BaseSpecification<BookCopy>
{
	public BookCopiesByBookIdSpecification(int bookId)
		: base((Expression<Func<BookCopy, bool>>)((BookCopy bc) => bc.BookId == bookId))
	{
	}
}
