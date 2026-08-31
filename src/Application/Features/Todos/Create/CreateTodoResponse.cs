namespace App.Application.Features.Todos.Create;

public sealed record CreateTodoResponse(Guid Id, string Title, string? Description);
