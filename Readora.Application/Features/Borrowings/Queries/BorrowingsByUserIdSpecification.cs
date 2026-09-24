using System;
using System.Linq.Expressions;
using Readora.Application.Specifications;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Borrowings.Queries;

public class BorrowingsByUserIdSpecification : BaseSpecification<Borrowing>
{
	public BorrowingsByUserIdSpecification(int userId)
		: base((Expression<Func<Borrowing, bool>>)((Borrowing b) => b.UserId == userId))
	{
		AddOrderByDescending((Borrowing b) => b.BorrowedAt);
	}
}
