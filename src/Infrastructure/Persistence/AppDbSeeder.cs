using App.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace App.Infrastructure.Persistence;

public static class AppDbSeeder {
    public static async Task SeedAsync(IServiceProvider serviceProvider) {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>(); // <-- Get config
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<AppDbContext>>();

        await context.Database.MigrateAsync();

        await SeedRolesAsync(roleManager, logger);
        await SeedAdminUserAsync(userManager, configuration, logger);
    }

    private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager, ILogger logger) {
        string[] roles = ["Admin", "User"];

        foreach (var role in roles) {
            if (!await roleManager.RoleExistsAsync(role)) {
                await roleManager.CreateAsync(new IdentityRole(role));
                logger.LogInformation("Created role: {Role}", role);
            }
        }
    }

    private static async Task SeedAdminUserAsync(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        ILogger logger
    ) {
        const string adminEmail = "admin@app.dev";

        if (await userManager.FindByEmailAsync(adminEmail) is not null) {
            return;
        }

        var adminPassword = configuration["Admin:Password"];

        if (string.IsNullOrEmpty(adminPassword)) {
            throw new InvalidOperationException(
                "Admin password is not configured. Please set the 'Admin:Password' environment variable or User Secret."
            );
        }

        var admin = new ApplicationUser {
            DisplayName = "ادمین",
            FullName = null,
            Email = adminEmail,
            UserName = adminEmail,
            EmailConfirmed = true,
        };

        var result = await userManager.CreateAsync(admin, adminPassword);

        if (result.Succeeded) {
            await userManager.AddToRoleAsync(admin, "Admin");
            logger.LogInformation("Seeded admin user: {Email}", adminEmail);
        }
    }
}
