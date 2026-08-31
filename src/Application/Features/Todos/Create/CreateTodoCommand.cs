using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Todos.Create;

public sealed record CreateTodoCommand(string Title, string? Description) : ICommand<Result<CreateTodoResponse>>;
