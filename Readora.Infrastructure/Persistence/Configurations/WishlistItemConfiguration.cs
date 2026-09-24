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
		builder.HasIndex((Expression<Func<WishlistItem, object>>)((WishlistItem wi) => new { wi.WishlistId, wi.BookId })).IsUnique(true);
		builder.HasOne<Wishlist>((Expression<Func<WishlistItem, Wishlist>>)((WishlistItem wi) => wi.Wishlist)).WithMany((Expression<Func<Wishlist, IEnumerable<WishlistItem>>>)((Wishlist w) => w.Items)).HasForeignKey((Expression<Func<WishlistItem, object>>)((WishlistItem wi) => wi.WishlistId))
			.OnDelete((DeleteBehavior)3);
		builder.HasOne<Book>((Expression<Func<WishlistItem, Book>>)((WishlistItem wi) => wi.Book)).WithMany((string)null).HasForeignKey((Expression<Func<WishlistItem, object>>)((WishlistItem wi) => wi.BookId))
			.OnDelete((DeleteBehavior)3);
	}
}
