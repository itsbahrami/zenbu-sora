using App.Domain.Models;
using App.Domain.Models.Jot;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Abstractions.Data;

public interface IAppDbContext {
    DbSet<TodoItem> Todos { get; }
    DbSet<Jotting> Jottings { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
