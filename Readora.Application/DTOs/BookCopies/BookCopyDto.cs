using Readora.Domain.Enums;

namespace Readora.Application.DTOs.BookCopies;

public class BookCopyDto
{
	public int Id { get; set; }

	public int BookId { get; set; }

	public BookCopyStatus Status { get; set; }
}
