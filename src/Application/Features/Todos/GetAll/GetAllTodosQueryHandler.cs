using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Application.Features.Todos.Get;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Todos.GetAll;

public sealed class GetAllTodosQueryHandler(
    IAppDbContext dbContext
) : IQueryHandler<GetAllTodosQuery, Result<PagedResult<TodoDetailResponse>>> {
    public async Task<Result<PagedResult<TodoDetailResponse>>> HandleAsync(
        GetAllTodosQuery query,
        CancellationToken cancellationToken = default
    ) {
        var totalCount = await dbContext.Todos.CountAsync(cancellationToken);

        var items = await dbContext.Todos
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .Select(t => new TodoDetailResponse {
                CompletedAt = t.CompletedAt,
                CreatedAt = t.CreatedAt,
                Description = t.Description,
                Id = t.Id,
                IsCompleted = t.IsCompleted,
                Title = t.Title,
            })
            .ToListAsync(cancellationToken);

        var pagedResult = new PagedResult<TodoDetailResponse>(items, totalCount, query.Page, query.PageSize);
        return Result.Success(pagedResult);
    }
}
