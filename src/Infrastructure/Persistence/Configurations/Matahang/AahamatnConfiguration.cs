using App.Domain.Models.Matahang;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Persistence.Configurations.Matahang;

public sealed class AahamatnConfiguration : IEntityTypeConfiguration<Aahamatn> {
    public void Configure(EntityTypeBuilder<Aahamatn> builder) {
        builder.Property(t => t.Id)
            .ValueGeneratedNever();
    }
}
