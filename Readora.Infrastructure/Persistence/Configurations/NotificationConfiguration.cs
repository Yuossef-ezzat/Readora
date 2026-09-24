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
		builder.Property<string>((Expression<Func<Notification, string>>)((Notification n) => n.Title)).IsRequired(true).HasMaxLength(200);
		builder.Property<string>((Expression<Func<Notification, string>>)((Notification n) => n.Message)).IsRequired(true).HasMaxLength(1000);
		builder.HasOne<ApplicationUser>((Expression<Func<Notification, ApplicationUser>>)((Notification n) => n.User)).WithMany((Expression<Func<ApplicationUser, IEnumerable<Notification>>>)((ApplicationUser u) => u.Notifications)).HasForeignKey((Expression<Func<Notification, object>>)((Notification n) => n.UserId))
			.OnDelete((DeleteBehavior)3);
	}
}
