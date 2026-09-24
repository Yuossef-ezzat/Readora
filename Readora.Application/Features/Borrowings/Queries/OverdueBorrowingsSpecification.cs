using System;
using System.Linq.Expressions;
using Readora.Application.Specifications;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Borrowings.Queries;

public class OverdueBorrowingsSpecification : BaseSpecification<Borrowing>
{
	public OverdueBorrowingsSpecification()
		: base((Expression<Func<Borrowing, bool>>)((Borrowing b) => b.ReturnedAt == null && b.DueDate < DateTime.UtcNow))
	{
	}
}
