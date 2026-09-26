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
		builder.Property(c => c.Name).IsRequired(true).HasMaxLength(100);
		builder.HasIndex(c => c.Name).IsUnique(true);
		builder.Property(c => c.Description).HasMaxLength(500);
	}
}
