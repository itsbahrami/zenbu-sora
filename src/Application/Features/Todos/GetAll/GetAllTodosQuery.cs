using App.Application.Abstractions.Messaging;
using App.Application.Features.Todos.Get;
using App.Domain.Common;

namespace App.Application.Features.Todos.GetAll;

public sealed record GetAllTodosQuery(int Page = 1, int PageSize = 10)
    : IQuery<Result<PagedResult<TodoDetailResponse>>>;
