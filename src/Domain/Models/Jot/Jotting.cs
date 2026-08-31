using App.Domain.Common;

namespace App.Domain.Models.Jot;

public sealed class Jotting : AggregateRoot, IHasOwner {
    public required Guid OwnerId { get; init; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}
