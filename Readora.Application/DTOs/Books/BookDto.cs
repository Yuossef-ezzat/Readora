using System;

namespace Readora.Application.DTOs.Books;

public class BookDto
{
	public int Id { get; set; }

	public string Title { get; set; } = string.Empty;

	public string ISBN { get; set; } = string.Empty;

	public string? Description { get; set; }

	public DateTime? PublishedDate { get; set; }

	public string? CoverImageUrl { get; set; }

	public string? FileKey { get; set; }

	public int AuthorId { get; set; }

	public int CategoryId { get; set; }
}
