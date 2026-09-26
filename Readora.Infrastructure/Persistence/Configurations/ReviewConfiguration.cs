using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
	public void Configure(EntityTypeBuilder<Review> builder)
	{
		builder.Property(r => r.Rating).IsRequired(true);
		builder.Property(r => r.Comment).HasMaxLength(1000);
		builder.HasIndex(r => new { r.UserId, r.BookId }).IsUnique(true);
		builder.HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId)
			.OnDelete((DeleteBehavior)1);
		builder.HasOne(r => r.Book).WithMany(b => b.Reviews).HasForeignKey(r => r.BookId)
			.OnDelete((DeleteBehavior)3);
	}
}
