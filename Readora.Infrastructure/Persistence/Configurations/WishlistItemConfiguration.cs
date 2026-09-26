using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class WishlistItemConfiguration : IEntityTypeConfiguration<WishlistItem>
{
	public void Configure(EntityTypeBuilder<WishlistItem> builder)
	{
		builder.HasIndex(wi => new { wi.WishlistId, wi.BookId }).IsUnique(true);
		builder.HasOne(wi => wi.Wishlist).WithMany(w => w.Items).HasForeignKey(wi => wi.WishlistId)
			.OnDelete((DeleteBehavior)3);
		builder.HasOne(wi => wi.Book).WithMany().HasForeignKey(wi => wi.BookId)
			.OnDelete((DeleteBehavior)3);
	}
}
