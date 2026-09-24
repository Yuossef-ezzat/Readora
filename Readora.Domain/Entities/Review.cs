using System;
using Readora.Domain.Common;

namespace Readora.Domain.Entities;

public class Review : BaseEntity
{
	public int UserId { get; set; }

	public ApplicationUser? User { get; set; }

	public int BookId { get; set; }

	public Book? Book { get; set; }

	public int Rating { get; set; }

	public string? Comment { get; set; }

	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
