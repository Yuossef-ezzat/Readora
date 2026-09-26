using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
	public void Configure(EntityTypeBuilder<ApplicationUser> builder)
	{
		builder.Property(u => u.FirstName).IsRequired(true).HasMaxLength(50);
		builder.Property(u => u.LastName).IsRequired(true).HasMaxLength(50);
		builder.HasOne( u => u.Wishlist).WithOne(w => w.User).HasForeignKey<Wishlist>(w => w.UserId)
			.OnDelete((DeleteBehavior)3);
	}
}
