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
		builder.Property<int>((Expression<Func<Review, int>>)((Review r) => r.Rating)).IsRequired(true);
		builder.Property<string>((Expression<Func<Review, string>>)((Review r) => r.Comment)).HasMaxLength(1000);
		builder.HasIndex((Expression<Func<Review, object>>)((Review r) => new { r.UserId, r.BookId })).IsUnique(true);
		builder.HasOne<ApplicationUser>((Expression<Func<Review, ApplicationUser>>)((Review r) => r.User)).WithMany((Expression<Func<ApplicationUser, IEnumerable<Review>>>)((ApplicationUser u) => u.Reviews)).HasForeignKey((Expression<Func<Review, object>>)((Review r) => r.UserId))
			.OnDelete((DeleteBehavior)1);
		builder.HasOne<Book>((Expression<Func<Review, Book>>)((Review r) => r.Book)).WithMany((Expression<Func<Book, IEnumerable<Review>>>)((Book b) => b.Reviews)).HasForeignKey((Expression<Func<Review, object>>)((Review r) => r.BookId))
			.OnDelete((DeleteBehavior)3);
	}
}
