using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using App.Domain.Models;

namespace App.Application.Features.Todos.Create;

public sealed class CreateTodoCommandHandler(IAppDbContext dbContext)
    : ICommandHandler<CreateTodoCommand, Result<CreateTodoResponse>> {
    public async Task<Result<CreateTodoResponse>> HandleAsync(
        CreateTodoCommand command,
        CancellationToken cancellationToken = default
    ) {
        var todo = new TodoItem {
            Title = command.Title,
            Description = command.Description
        };

        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new CreateTodoResponse(todo.Id, todo.Title, todo.Description);
        return Result.Success(response);
    }
}
