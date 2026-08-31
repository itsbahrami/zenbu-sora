using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using App.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Todos.Delete;

public sealed class DeleteTodoCommandHandler(IAppDbContext dbContext) : ICommandHandler<DeleteTodoCommand> {
    public async Task<Result> HandleAsync(DeleteTodoCommand command, CancellationToken cancellationToken = default) {
        var todo = await dbContext.Todos
            .FirstOrDefaultAsync(t => t.Id == command.Id, cancellationToken);
        if (todo is null)
            return Result.Failure(Error.NotFound("Todo.NotFound", $"Todo with ID '{command.Id}' was not found."));

        dbContext.Todos.Remove(todo);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
