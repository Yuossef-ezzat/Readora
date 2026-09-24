using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence.Configurations;

public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
	public void Configure(EntityTypeBuilder<Wishlist> builder)
	{
	}
}
