namespace App.Application.Features.Todos.Get;

public sealed record TodoDetailResponse {
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string? Description { get; init; }
    public required bool IsCompleted { get; init; }
    public required DateTimeOffset? CompletedAt { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}
