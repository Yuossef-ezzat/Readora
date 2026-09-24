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
		builder.HasOne<ApplicationUser>((Expression<Func<Borrowing, ApplicationUser>>)((Borrowing b) => b.User)).WithMany((Expression<Func<ApplicationUser, IEnumerable<Borrowing>>>)((ApplicationUser u) => u.Borrowings)).HasForeignKey((Expression<Func<Borrowing, object>>)((Borrowing b) => b.UserId))
			.OnDelete((DeleteBehavior)1);
		builder.HasOne<BookCopy>((Expression<Func<Borrowing, BookCopy>>)((Borrowing b) => b.BookCopy)).WithMany((Expression<Func<BookCopy, IEnumerable<Borrowing>>>)((BookCopy bc) => bc.Borrowings)).HasForeignKey((Expression<Func<Borrowing, object>>)((Borrowing b) => b.BookCopyId))
			.OnDelete((DeleteBehavior)1);
	}
}
