using App.Application.Abstractions.Messaging;

namespace App.Application.Features.Todos.Complete;

public sealed record CompleteTodoCommand(Guid Id) : ICommand;
