using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Todos.Get;

public sealed record GetTodoQuery(Guid Id) : IQuery<Result<TodoDetailResponse>>;
