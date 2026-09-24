using System.Collections.Generic;
using Readora.Domain.Common;

namespace Readora.Domain.Entities;

public class Author : BaseEntity
{
	public string Name { get; set; } = string.Empty;

	public string? Bio { get; set; }

	public ICollection<Book> Books { get; set; } = new List<Book>();
}
