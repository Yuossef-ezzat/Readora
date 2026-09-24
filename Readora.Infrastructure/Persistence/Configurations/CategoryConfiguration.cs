using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
	public void Configure(EntityTypeBuilder<Category> builder)
	{
		builder.Property<string>((Expression<Func<Category, string>>)((Category c) => c.Name)).IsRequired(true).HasMaxLength(100);
		builder.HasIndex((Expression<Func<Category, object>>)((Category c) => c.Name)).IsUnique(true);
		builder.Property<string>((Expression<Func<Category, string>>)((Category c) => c.Description)).HasMaxLength(500);
	}
}
