using App.Domain.Common;

namespace App.Domain.Models;

public sealed class TodoItem : Entity {
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }

    public void MarkAsCompleted() {
        if (IsCompleted) return;
        IsCompleted = true;
        CompletedAt = DateTimeOffset.UtcNow;
    }

    public void MarkAsIncomplete() {
        IsCompleted = false;
        CompletedAt = null;
    }
}
