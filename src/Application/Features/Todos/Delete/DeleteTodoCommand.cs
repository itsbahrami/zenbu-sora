using App.Application.Abstractions.Messaging;

namespace App.Application.Features.Todos.Delete;

public sealed record DeleteTodoCommand(Guid Id) : ICommand;
