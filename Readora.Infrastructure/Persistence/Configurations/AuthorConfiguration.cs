using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
	public void Configure(EntityTypeBuilder<Author> builder)
	{
		builder.Property<string>((Expression<Func<Author, string>>)((Author a) => a.Name)).IsRequired(true).HasMaxLength(100);
		builder.Property<string>((Expression<Func<Author, string>>)((Author a) => a.Bio)).HasMaxLength(1000);
	}
}
