using System;
using System.Linq.Expressions;
using Readora.Application.Specifications;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Borrowings.Commands;

public class AvailableBookCopySpecification : BaseSpecification<BookCopy>
{
	public AvailableBookCopySpecification(int bookId)
		: base((Expression<Func<BookCopy, bool>>)((BookCopy bc) => bc.BookId == bookId && (int)bc.Status == 0))
	{
	}
}
