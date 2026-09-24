using System;
using System.Collections.Generic;
using Readora.Domain.Common;

namespace Readora.Domain.Entities;

public class Book : BaseEntity
{
	public string Title { get; set; } = string.Empty;

	public string ISBN { get; set; } = string.Empty;

	public string? Description { get; set; }

	public DateTime? PublishedDate { get; set; }

	public string? CoverImageUrl { get; set; }

	public string? FileKey { get; set; }

	public int AuthorId { get; set; }

	public Author? Author { get; set; }

	public int CategoryId { get; set; }

	public Category? Category { get; set; }

	public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();

	public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
