using System;
using Readora.Domain.Common;
using Readora.Domain.Enums;

namespace Readora.Domain.Entities;

public class Borrowing : BaseEntity
{
	public int UserId { get; set; }

	public ApplicationUser? User { get; set; }

	public int BookCopyId { get; set; }

	public BookCopy? BookCopy { get; set; }

	public DateTime BorrowedAt { get; set; }

	public DateTime DueDate { get; set; }

	public DateTime? ReturnedAt { get; set; }

	public BorrowingStatus Status { get; set; } = BorrowingStatus.Active;
}
