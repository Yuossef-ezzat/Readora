using System.Collections.Generic;
using Readora.Domain.Common;

namespace Readora.Domain.Entities;

public class Wishlist : BaseEntity
{
	public int UserId { get; set; }

	public ApplicationUser? User { get; set; }

	public ICollection<WishlistItem> Items { get; set; } = new List<WishlistItem>();
}
