using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class BookCopyConfiguration : IEntityTypeConfiguration<BookCopy>
{
	public void Configure(EntityTypeBuilder<BookCopy> builder)
	{
		builder.Property<string>((Expression<Func<BookCopy, string>>)((BookCopy bc) => bc.CopyNumber)).IsRequired(true).HasMaxLength(50);
		builder.HasIndex((Expression<Func<BookCopy, object>>)((BookCopy bc) => new { bc.BookId, bc.CopyNumber })).IsUnique(true);
		builder.HasOne<Book>((Expression<Func<BookCopy, Book>>)((BookCopy bc) => bc.Book)).WithMany((Expression<Func<Book, IEnumerable<BookCopy>>>)((Book b) => b.Copies)).HasForeignKey((Expression<Func<BookCopy, object>>)((BookCopy bc) => bc.BookId))
			.OnDelete((DeleteBehavior)3);
		builder.Property<byte[]>((Expression<Func<BookCopy, byte[]>>)((BookCopy bc) => bc.RowVersion)).IsRowVersion().IsRequired(true);
	}
}
