using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Readora.Domain.Entities;

public class ApplicationUser : IdentityUser<int>
{
	public string FirstName { get; set; } = string.Empty;

	public string LastName { get; set; } = string.Empty;

	public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

	public ICollection<Review> Reviews { get; set; } = new List<Review>();

	public Wishlist? Wishlist { get; set; }

	public ICollection<Notification> Notifications { get; set; } = new List<Notification>();

	public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
