using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Readora.Domain.Entities;
using Readora.Domain.Enums;

namespace Readora.Infrastructure.Persistence;

public static class DatabaseSeeder
{
    public static async Task SeedAllAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await SeedRolesAsync(serviceProvider);
        await SeedUsersAsync(serviceProvider);
        await SeedCategoriesAsync(context);
        await SeedAuthorsAsync(context);
        await SeedBooksAsync(context);
        await SeedBookCopiesAsync(context);
        await SeedBorrowingsAsync(context);
        await SeedReviewsAsync(context);
        await SeedWishlistsAsync(context);
        await SeedNotificationsAsync(context);
    }

    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseSeeder");

        string[] roles = { "Admin", "Member", "Librarian" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole<int>(role));
                if (result.Succeeded)
                {
                    logger.LogInformation("Seeded role: {Role}", role);
                }
                else
                {
                    logger.LogError("Error seeding role {Role}: {Errors}", role, string.Join(", ", result.Errors));
                }
            }
        }
    }

    private static async Task SeedUsersAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        
        if (!userManager.Users.Any())
        {
            var admin = new ApplicationUser { UserName = "admin@readora.com", Email = "admin@readora.com", FirstName = "Admin", LastName = "User", EmailConfirmed = true };
            var result = await userManager.CreateAsync(admin, "Admin@123");

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(e => e.Description))
                );
            }

            Console.WriteLine($"SecurityStamp: {admin.SecurityStamp}");
            await userManager.AddToRoleAsync(admin, "Admin");

            var librarian = new ApplicationUser { UserName = "librarian@readora.com", Email = "librarian@readora.com", FirstName = "Librarian", LastName = "User", EmailConfirmed = true };
            var resultLibrarian = await userManager.CreateAsync(librarian, "Libb@123");
            if (!resultLibrarian.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", resultLibrarian.Errors.Select(e => e.Description))
                );
            }
            await userManager.AddToRoleAsync(librarian, "Librarian");

            var alice = new ApplicationUser { UserName = "alice@readora.com", Email = "alice@readora.com", FirstName = "Alice", LastName = "Smith", EmailConfirmed = true };
            var resultAlice = await userManager.CreateAsync(alice, "Member@123");
            if (!resultAlice.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", resultAlice.Errors.Select(e => e.Description))
                );
            }
            await userManager.AddToRoleAsync(alice, "Member");

            var bob = new ApplicationUser { UserName = "bob@readora.com", Email = "bob@readora.com", FirstName = "Bob", LastName = "Johnson", EmailConfirmed = true };
            var resultBob = await userManager.CreateAsync(bob, "Member@123");
            if (!resultBob.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", resultBob.Errors.Select(e => e.Description))
                );
            }
            await userManager.AddToRoleAsync(bob, "Member");

            var carol = new ApplicationUser { UserName = "carol@readora.com", Email = "carol@readora.com", FirstName = "Carol", LastName = "Williams", EmailConfirmed = true };
            var resultCarol = await userManager.CreateAsync(carol, "Member@123");
            if (!resultCarol.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", resultCarol.Errors.Select(e => e.Description))
                );
            }
            await userManager.AddToRoleAsync(carol, "Member");
        }
    }

    private static async Task SeedCategoriesAsync(ApplicationDbContext context)
    {
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Name = "Fiction", Description = "Fictional literature" },
                new Category { Name = "Science & Technology", Description = "Books about science and tech" },
                new Category { Name = "History & Biography", Description = "Historical books and biographies" },
                new Category { Name = "Self-Development", Description = "Self-improvement books" },
                new Category { Name = "Children's Books", Description = "Books for kids" }
            );
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedAuthorsAsync(ApplicationDbContext context)
    {
        if (!context.Authors.Any())
        {
            context.Authors.AddRange(
                new Author { Name = "George Orwell", Bio = "English novelist, known for 1984 and Animal Farm." },
                new Author { Name = "Yuval Noah Harari", Bio = "Israeli historian and author of Sapiens." },
                new Author { Name = "Robert C. Martin", Bio = "Software engineer, author of Clean Code." },
                new Author { Name = "J.K. Rowling", Bio = "British author of the Harry Potter series." },
                new Author { Name = "Dale Carnegie", Bio = "American writer and lecturer on self-improvement." }
            );
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedBooksAsync(ApplicationDbContext context)
    {
        if (!context.Books.Any())
        {
            var orwell = await context.Authors.FirstAsync(a => a.Name == "George Orwell");
            var harari = await context.Authors.FirstAsync(a => a.Name == "Yuval Noah Harari");
            var martin = await context.Authors.FirstAsync(a => a.Name == "Robert C. Martin");
            var rowling = await context.Authors.FirstAsync(a => a.Name == "J.K. Rowling");
            var carnegie = await context.Authors.FirstAsync(a => a.Name == "Dale Carnegie");

            var fiction = await context.Categories.FirstAsync(c => c.Name == "Fiction");
            var history = await context.Categories.FirstAsync(c => c.Name == "History & Biography");
            var sciTech = await context.Categories.FirstAsync(c => c.Name == "Science & Technology");
            var selfDev = await context.Categories.FirstAsync(c => c.Name == "Self-Development");

            context.Books.AddRange(
                new Book { Title = "1984", ISBN = "978-0451524935", AuthorId = orwell.Id, CategoryId = fiction.Id, PublishedDate = new DateTime(1949, 6, 8).ToUniversalTime() },
                new Book { Title = "Sapiens", ISBN = "978-0062316097", AuthorId = harari.Id, CategoryId = history.Id, PublishedDate = new DateTime(2011, 1, 1).ToUniversalTime() },
                new Book { Title = "Clean Code", ISBN = "978-0132350884", AuthorId = martin.Id, CategoryId = sciTech.Id, PublishedDate = new DateTime(2008, 8, 1).ToUniversalTime() },
                new Book { Title = "Harry Potter & Stone", ISBN = "978-0439708180", AuthorId = rowling.Id, CategoryId = fiction.Id, PublishedDate = new DateTime(1997, 6, 26).ToUniversalTime() },
                new Book { Title = "How to Win Friends", ISBN = "978-0671027032", AuthorId = carnegie.Id, CategoryId = selfDev.Id, PublishedDate = new DateTime(1936, 10, 1).ToUniversalTime() }
            );
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedBookCopiesAsync(ApplicationDbContext context)
    {
        if (!context.BookCopies.Any())
        {
            var books = await context.Books.ToListAsync();
            foreach (var book in books)
            {
                context.BookCopies.AddRange(
                    new BookCopy { BookId = book.Id, CopyNumber = $"{book.ISBN}-001", Status = BookCopyStatus.Available },
                    new BookCopy { BookId = book.Id, CopyNumber = $"{book.ISBN}-002", Status = BookCopyStatus.Available },
                    new BookCopy { BookId = book.Id, CopyNumber = $"{book.ISBN}-003", Status = BookCopyStatus.Available }
                );
            }
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedBorrowingsAsync(ApplicationDbContext context)
    {
        if (!context.Borrowings.Any())
        {
            var alice = await context.Users.FirstAsync(u => u.Email == "alice@readora.com");
            var bob = await context.Users.FirstAsync(u => u.Email == "bob@readora.com");
            var carol = await context.Users.FirstAsync(u => u.Email == "carol@readora.com");

            var book1984 = await context.Books.FirstAsync(b => b.Title == "1984");
            var copy1984 = await context.BookCopies.FirstAsync(bc => bc.BookId == book1984.Id && bc.CopyNumber.EndsWith("-001"));
            
            var sapiens = await context.Books.FirstAsync(b => b.Title == "Sapiens");
            var copySapiens = await context.BookCopies.FirstAsync(bc => bc.BookId == sapiens.Id && bc.CopyNumber.EndsWith("-001"));

            var cleanCode = await context.Books.FirstAsync(b => b.Title == "Clean Code");
            var copyCleanCode = await context.BookCopies.FirstAsync(bc => bc.BookId == cleanCode.Id && bc.CopyNumber.EndsWith("-001"));

            // Alice's active borrowing
            context.Borrowings.Add(new Borrowing
            {
                UserId = alice.Id,
                BookCopyId = copy1984.Id,
                BorrowedAt = DateTime.UtcNow.AddDays(-7),
                DueDate = DateTime.UtcNow.AddDays(7),
                Status = BorrowingStatus.Active
            });
            copy1984.Status = BookCopyStatus.Borrowed;

            // Bob's returned borrowing
            context.Borrowings.Add(new Borrowing
            {
                UserId = bob.Id,
                BookCopyId = copySapiens.Id,
                BorrowedAt = DateTime.UtcNow.AddDays(-30),
                DueDate = DateTime.UtcNow.AddDays(-16),
                ReturnedAt = DateTime.UtcNow.AddDays(-10),
                Status = BorrowingStatus.Returned
            });

            // Carol's overdue borrowing
            context.Borrowings.Add(new Borrowing
            {
                UserId = carol.Id,
                BookCopyId = copyCleanCode.Id,
                BorrowedAt = DateTime.UtcNow.AddDays(-20),
                DueDate = DateTime.UtcNow.AddDays(-6),
                Status = BorrowingStatus.Overdue
            });
            copyCleanCode.Status = BookCopyStatus.Borrowed;

            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedReviewsAsync(ApplicationDbContext context)
    {
        if (!context.Reviews.Any())
        {
            var alice = await context.Users.FirstAsync(u => u.Email == "alice@readora.com");
            var bob = await context.Users.FirstAsync(u => u.Email == "bob@readora.com");
            var carol = await context.Users.FirstAsync(u => u.Email == "carol@readora.com");

            var book1984 = await context.Books.FirstAsync(b => b.Title == "1984");
            var sapiens = await context.Books.FirstAsync(b => b.Title == "Sapiens");
            var cleanCode = await context.Books.FirstAsync(b => b.Title == "Clean Code");

            context.Reviews.AddRange(
                new Review { UserId = alice.Id, BookId = book1984.Id, Rating = 5, Comment = "A must-read dystopian masterpiece!", CreatedAt = DateTime.UtcNow.AddDays(-5) },
                new Review { UserId = bob.Id, BookId = sapiens.Id, Rating = 4, Comment = "Incredibly thought-provoking.", CreatedAt = DateTime.UtcNow.AddDays(-2) },
                new Review { UserId = carol.Id, BookId = cleanCode.Id, Rating = 5, Comment = "Essential reading for every developer.", CreatedAt = DateTime.UtcNow.AddDays(-1) }
            );

            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedWishlistsAsync(ApplicationDbContext context)
    {
        if (!context.Wishlists.Any())
        {
            var alice = await context.Users.FirstAsync(u => u.Email == "alice@readora.com");
            var bob = await context.Users.FirstAsync(u => u.Email == "bob@readora.com");

            var sapiens = await context.Books.FirstAsync(b => b.Title == "Sapiens");
            var harryPotter = await context.Books.FirstAsync(b => b.Title == "Harry Potter & Stone");
            var cleanCode = await context.Books.FirstAsync(b => b.Title == "Clean Code");

            var aliceWishlist = new Wishlist
            {
                UserId = alice.Id,
                Items = new List<WishlistItem>
                {
                    new WishlistItem { BookId = sapiens.Id },
                    new WishlistItem { BookId = harryPotter.Id }
                }
            };

            var bobWishlist = new Wishlist
            {
                UserId = bob.Id,
                Items = new List<WishlistItem>
                {
                    new WishlistItem { BookId = cleanCode.Id }
                }
            };

            context.Wishlists.AddRange(aliceWishlist, bobWishlist);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedNotificationsAsync(ApplicationDbContext context)
    {
        if (!context.Notifications.Any())
        {
            var alice = await context.Users.FirstAsync(u => u.Email == "alice@readora.com");
            var bob = await context.Users.FirstAsync(u => u.Email == "bob@readora.com");
            var carol = await context.Users.FirstAsync(u => u.Email == "carol@readora.com");

            context.Notifications.AddRange(
                new Notification { UserId = alice.Id, Title = "Borrowing Reminder", Message = "Your book '1984' is due in 7 days.", Type = NotificationType.BorrowingReminder },
                new Notification { UserId = carol.Id, Title = "Overdue Book", Message = "Your book 'Clean Code' is overdue!", Type = NotificationType.BorrowingOverdue },
                new Notification { UserId = bob.Id, Title = "Welcome", Message = "Welcome to Readora Library!", Type = NotificationType.General },
                new Notification { UserId = alice.Id, Title = "New Book Available", Message = "A new book has been added to your category.", Type = NotificationType.BookAvailable }
            );

            await context.SaveChangesAsync();
        }
    }
}
