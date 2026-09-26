using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
	public void Configure(EntityTypeBuilder<Notification> builder)
	{
		builder.Property(n => n.Title).IsRequired(true).HasMaxLength(200);
		builder.Property(n => n.Message).IsRequired(true).HasMaxLength(1000);
		builder.HasOne(n => n.User).WithMany(u => u.Notifications).HasForeignKey(n => n.UserId)
			.OnDelete((DeleteBehavior)3);
	}
}
