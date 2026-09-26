using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class BorrowingConfiguration : IEntityTypeConfiguration<Borrowing>
{
	public void Configure(EntityTypeBuilder<Borrowing> builder)
	{
		builder.HasOne(b => b.User).WithMany(u => u.Borrowings).HasForeignKey(b => b.UserId)
			.OnDelete((DeleteBehavior)1);
		builder.HasOne(b => b.BookCopy).WithMany(bc => bc.Borrowings).HasForeignKey(b => b.BookCopyId)
			.OnDelete((DeleteBehavior)1);
	}
}
