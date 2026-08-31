using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Persistence.Configurations;

public sealed class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser> {
    public void Configure(EntityTypeBuilder<ApplicationUser> builder) {
        builder.Property(u => u.DisplayName)
            .HasMaxLength(100);

        builder.Property(u => u.FullName)
            .HasMaxLength(100);

        builder.Property(u => u.RefreshToken)
            .HasMaxLength(500);
    }
}
