using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
	public DbSet<Author> Authors => ((DbContext)this).Set<Author>();

	public DbSet<Category> Categories => ((DbContext)this).Set<Category>();

	public DbSet<Book> Books => ((DbContext)this).Set<Book>();

	public DbSet<BookCopy> BookCopies => ((DbContext)this).Set<BookCopy>();

	public DbSet<Borrowing> Borrowings => ((DbContext)this).Set<Borrowing>();

	public DbSet<Review> Reviews => ((DbContext)this).Set<Review>();

	public DbSet<Wishlist> Wishlists => ((DbContext)this).Set<Wishlist>();

	public DbSet<WishlistItem> WishlistItems => ((DbContext)this).Set<WishlistItem>();

	public DbSet<Notification> Notifications => ((DbContext)this).Set<Notification>();

	public DbSet<RefreshToken> RefreshTokens => ((DbContext)this).Set<RefreshToken>();

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base((DbContextOptions)(object)options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly, (Func<Type, bool>)null);
	}
}
