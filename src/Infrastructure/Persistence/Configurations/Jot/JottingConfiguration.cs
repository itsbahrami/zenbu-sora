using App.Domain.Models.Jot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Persistence.Configurations.Jot;

public sealed class JottingConfiguration : IEntityTypeConfiguration<Jotting> {
    public void Configure(EntityTypeBuilder<Jotting> builder) {
        builder.Property(t => t.Id)
            .ValueGeneratedNever();
    }
}
