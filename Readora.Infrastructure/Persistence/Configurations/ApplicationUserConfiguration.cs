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
		builder.Property<string>((Expression<Func<ApplicationUser, string>>)((ApplicationUser u) => u.FirstName)).IsRequired(true).HasMaxLength(50);
		builder.Property<string>((Expression<Func<ApplicationUser, string>>)((ApplicationUser u) => u.LastName)).IsRequired(true).HasMaxLength(50);
		builder.HasOne<Wishlist>((Expression<Func<ApplicationUser, Wishlist>>)((ApplicationUser u) => u.Wishlist)).WithOne((Expression<Func<Wishlist, ApplicationUser>>)((Wishlist w) => w.User)).HasForeignKey<Wishlist>((Expression<Func<Wishlist, object>>)((Wishlist w) => w.UserId))
			.OnDelete((DeleteBehavior)3);
	}
}
