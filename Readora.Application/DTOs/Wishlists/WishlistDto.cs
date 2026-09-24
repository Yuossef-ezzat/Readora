using System.Collections.Generic;

namespace Readora.Application.DTOs.Wishlists;

public class WishlistDto
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public List<WishlistItemDto> Items { get; set; } = new List<WishlistItemDto>();
}
