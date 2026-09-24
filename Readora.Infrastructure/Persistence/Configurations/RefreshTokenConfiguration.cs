using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
	public void Configure(EntityTypeBuilder<RefreshToken> builder)
	{
		builder.Property<string>((Expression<Func<RefreshToken, string>>)((RefreshToken rt) => rt.TokenHash)).IsRequired(true).HasMaxLength(256);
		builder.HasIndex((Expression<Func<RefreshToken, object>>)((RefreshToken rt) => rt.TokenHash));
		builder.HasOne<ApplicationUser>((Expression<Func<RefreshToken, ApplicationUser>>)((RefreshToken rt) => rt.User)).WithMany((Expression<Func<ApplicationUser, IEnumerable<RefreshToken>>>)((ApplicationUser u) => u.RefreshTokens)).HasForeignKey((Expression<Func<RefreshToken, object>>)((RefreshToken rt) => rt.UserId))
			.OnDelete((DeleteBehavior)3);
	}
}
