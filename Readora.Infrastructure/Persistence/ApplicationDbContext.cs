using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Readora.Domain.Entities;

namespace Readora.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
	public DbSet<Author> Authors => Set<Author>();

	public DbSet<Category> Categories => Set<Category>();

	public DbSet<Book> Books => Set<Book>();

	public DbSet<BookCopy> BookCopies => Set<BookCopy>();
	public DbSet<Borrowing> Borrowings => Set<Borrowing>();

	public DbSet<Review> Reviews => Set<Review>();

	public DbSet<Wishlist> Wishlists => Set<Wishlist>();

	public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();

	public DbSet<Notification> Notifications => Set<Notification>();

	public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
	}
}
