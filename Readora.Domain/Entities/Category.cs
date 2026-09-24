using System.Collections.Generic;
using Readora.Domain.Common;

namespace Readora.Domain.Entities;

public class Category : BaseEntity
{
	public string Name { get; set; } = string.Empty;

	public string? Description { get; set; }

	public ICollection<Book> Books { get; set; } = new List<Book>();
}
