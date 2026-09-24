using System;
using Readora.Domain.Common;

namespace Readora.Domain.Entities;

public class WishlistItem : BaseEntity
{
	public int WishlistId { get; set; }

	public Wishlist? Wishlist { get; set; }

	public int BookId { get; set; }

	public Book? Book { get; set; }

	public DateTime AddedAt { get; set; } = DateTime.UtcNow;
}
