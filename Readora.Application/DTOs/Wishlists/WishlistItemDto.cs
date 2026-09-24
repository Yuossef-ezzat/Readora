using System;

namespace Readora.Application.DTOs.Wishlists;

public class WishlistItemDto
{
	public int Id { get; set; }

	public int BookId { get; set; }

	public DateTime AddedAt { get; set; }
}
