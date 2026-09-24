using System;
using System.Linq.Expressions;
using Readora.Application.Specifications;
using Readora.Domain.Entities;

namespace Readora.Application.Features.Wishlists.Commands;

public class WishlistWithItemsSpecification : BaseSpecification<Wishlist>
{
	public WishlistWithItemsSpecification(int userId)
		: base((Expression<Func<Wishlist, bool>>)((Wishlist w) => w.UserId == userId))
	{
		AddInclude((Wishlist w) => w.Items);
	}
}
