using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
	public void Configure(EntityTypeBuilder<Book> builder)
	{
		builder.Property<string>((Expression<Func<Book, string>>)((Book b) => b.Title)).IsRequired(true).HasMaxLength(255);
		builder.Property<string>((Expression<Func<Book, string>>)((Book b) => b.ISBN)).IsRequired(true).HasMaxLength(20);
		builder.HasIndex((Expression<Func<Book, object>>)((Book b) => b.ISBN)).IsUnique(true);
		builder.Property<string>((Expression<Func<Book, string>>)((Book b) => b.Description)).HasMaxLength(2000);
		builder.HasOne<Author>((Expression<Func<Book, Author>>)((Book b) => b.Author)).WithMany((Expression<Func<Author, IEnumerable<Book>>>)((Author a) => a.Books)).HasForeignKey((Expression<Func<Book, object>>)((Book b) => b.AuthorId))
			.OnDelete((DeleteBehavior)1);
		builder.HasOne<Category>((Expression<Func<Book, Category>>)((Book b) => b.Category)).WithMany((Expression<Func<Category, IEnumerable<Book>>>)((Category c) => c.Books)).HasForeignKey((Expression<Func<Book, object>>)((Book b) => b.CategoryId))
			.OnDelete((DeleteBehavior)1);
	}
}
