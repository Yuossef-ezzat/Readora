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
		builder.Property(b => b.Title).IsRequired(true).HasMaxLength(255);
		builder.Property(b => b.ISBN).IsRequired(true).HasMaxLength(20);
		builder.HasIndex(b => b.ISBN).IsUnique(true);
		builder.Property(b => b.Description).HasMaxLength(2000);
		builder.HasOne(b => b.Author).WithMany(a => a.Books).HasForeignKey(b => b.AuthorId)
			.OnDelete((DeleteBehavior)1);
		builder.HasOne(b => b.Category).WithMany(c => c.Books).HasForeignKey(b => b.CategoryId)
			.OnDelete((DeleteBehavior)1);
	}
}
