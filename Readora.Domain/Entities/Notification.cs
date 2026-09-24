using System;
using Readora.Domain.Common;
using Readora.Domain.Enums;

namespace Readora.Domain.Entities;

public class Notification : BaseEntity
{
	public int UserId { get; set; }

	public ApplicationUser? User { get; set; }

	public string Title { get; set; } = string.Empty;

	public string Message { get; set; } = string.Empty;

	public bool IsRead { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

	public NotificationType Type { get; set; }
}
