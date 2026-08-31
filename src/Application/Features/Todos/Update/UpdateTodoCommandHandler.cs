using App.Application.Abstractions.Data;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using App.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Application.Features.Todos.Update;

public sealed class UpdateTodoCommandHandler(IAppDbContext dbContext) : ICommandHandler<UpdateTodoCommand> {
    public async Task<Result> HandleAsync(UpdateTodoCommand command, CancellationToken cancellationToken = default) {
        var todo = await dbContext.Todos
            .FirstOrDefaultAsync(t => t.Id == command.Id, cancellationToken);
        if (todo is null)
            return Result.Failure(Error.NotFound("Todo.NotFound", $"Todo with ID '{command.Id}' was not found."));

        todo.Title = command.Title;
        todo.Description = command.Description;

        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
