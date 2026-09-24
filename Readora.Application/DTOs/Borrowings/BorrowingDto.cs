using System;
using Readora.Domain.Enums;

namespace Readora.Application.DTOs.Borrowings;

public class BorrowingDto
{
	public int Id { get; set; }

	public int UserId { get; set; }

	public int BookCopyId { get; set; }

	public DateTime BorrowedAt { get; set; }

	public DateTime DueDate { get; set; }

	public DateTime? ReturnedAt { get; set; }

	public BorrowingStatus Status { get; set; }
}
