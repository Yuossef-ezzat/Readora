using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Readora.Infrastructure.Persistence;

public static class DatabaseSeeder
{
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
}
