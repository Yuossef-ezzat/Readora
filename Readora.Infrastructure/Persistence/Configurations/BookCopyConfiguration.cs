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
		builder.Property(bc => bc.CopyNumber).IsRequired(true).HasMaxLength(50);
		builder.HasIndex(bc => new { bc.BookId, bc.CopyNumber }).IsUnique(true);
		builder.HasOne(bc => bc.Book).WithMany(b => b.Copies).HasForeignKey(bc => bc.BookId)
			.OnDelete(DeleteBehavior.Cascade);
		builder.Property(bc => bc.RowVersion).IsConcurrencyToken().ValueGeneratedNever().IsRequired(true);
	}
}
