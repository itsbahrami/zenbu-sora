using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using App.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Todos.Get;

public sealed class GetTodoQueryHandler(
    IAppDbContext dbContext
) : IQueryHandler<GetTodoQuery, Result<TodoDetailResponse>> {
    public async Task<Result<TodoDetailResponse>> HandleAsync(
        GetTodoQuery query,
        CancellationToken cancellationToken = default
    ) {
        var todo = await dbContext.Todos
            .FirstOrDefaultAsync(t => t.Id == query.Id, cancellationToken);
        if (todo is null) {
            return Result.Failure<TodoDetailResponse>(
                Error.NotFound(
                    "Todo.NotFound",
                    $"Todo with ID '{query.Id}' was not found."
                )
            );
        }

        var response = new TodoDetailResponse {
            CompletedAt = todo.CompletedAt,
            CreatedAt = todo.CreatedAt,
            Description = todo.Description,
            Id = todo.Id,
            IsCompleted = todo.IsCompleted,
            Title = todo.Title,
        };

        return Result.Success(response);
    }
}
