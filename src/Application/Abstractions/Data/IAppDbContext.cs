using App.Domain.Models.Jot;
using App.Domain.Models.Matahang;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Abstractions.Data;

public interface IAppDbContext {
    DbSet<Jotting> Jottings { get; }
    DbSet<Aahamatn> Aahamatns { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
