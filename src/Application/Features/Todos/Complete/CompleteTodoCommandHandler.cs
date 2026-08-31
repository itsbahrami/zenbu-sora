using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Todos.Complete;

public sealed class CompleteTodoCommandHandler(IAppDbContext dbContext) : ICommandHandler<CompleteTodoCommand> {
    public async Task<Result> HandleAsync(CompleteTodoCommand command, CancellationToken cancellationToken = default) {
        var todo = await dbContext.Todos
            .FirstOrDefaultAsync(t => t.Id == command.Id, cancellationToken);
        if (todo is null)
            return Result.Failure(Error.NotFound("Todo.NotFound", $"Todo with ID '{command.Id}' was not found."));

        todo.MarkAsCompleted();
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
