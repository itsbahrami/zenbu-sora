using App.Application.Abstractions.Data;
using App.Domain.Common;
using App.Domain.Models;
using App.Domain.Models.Jot;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Persistence;

public sealed class AppDbContext(
    DbContextOptions<AppDbContext> options
) : IdentityDbContext<ApplicationUser>(options), IAppDbContext {
    public DbSet<TodoItem> Todos => Set<TodoItem>();
    public DbSet<Jotting> Jottings => Set<Jotting>();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
        UpdateAuditableEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    private void UpdateAuditableEntities() {
        var entries = ChangeTracker.Entries<Entity>();

        foreach (var entry in entries) {
            if (entry.State == EntityState.Modified) {
                entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
            }
        }
    }
}
