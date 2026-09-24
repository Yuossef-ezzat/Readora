using System;
using Readora.Domain.Common;

namespace Readora.Domain.Entities;

public class RefreshToken : BaseEntity
{
	public int UserId { get; set; }

	public ApplicationUser? User { get; set; }

	public string TokenHash { get; set; } = string.Empty;

	public DateTime ExpiresAt { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public DateTime? RevokedAt { get; set; }

	public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

	public bool IsRevoked => RevokedAt.HasValue;

	public bool IsActive => !IsRevoked && !IsExpired;
}
