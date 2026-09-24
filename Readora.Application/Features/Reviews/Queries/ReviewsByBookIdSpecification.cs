using System;
using System.Linq.Expressions;
using Readora.Application.Specifications;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Reviews.Queries;

public class ReviewsByBookIdSpecification : BaseSpecification<Review>
{
	public ReviewsByBookIdSpecification(int bookId)
		: base((Expression<Func<Review, bool>>)((Review r) => r.BookId == bookId))
	{
		AddOrderByDescending((Review r) => r.CreatedAt);
	}
}
