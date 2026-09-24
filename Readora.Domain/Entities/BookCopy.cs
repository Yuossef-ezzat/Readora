using System;
using System.Collections.Generic;
using Readora.Domain.Common;
using Readora.Domain.Enums;

namespace Readora.Domain.Entities;

public class BookCopy : BaseEntity
{
	public int BookId { get; set; }

	public Book? Book { get; set; }

	public string CopyNumber { get; set; } = string.Empty;

	public BookCopyStatus Status { get; set; } = BookCopyStatus.Available;

	public byte[] RowVersion { get; set; } = Array.Empty<byte>();

	public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}
