using App.Domain.Models.Jot;

namespace App.Application.Features.Jot.Jottings;

public sealed record JottingResponse {
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required string Content { get; init; }
    public required DateTimeOffset? UpdatedAt { get; init; }

    public static JottingResponse FromDomain(Jotting jotting) => new() {
        Id = jotting.Id,
        Title = jotting.Title,
        Content = jotting.Content,
        UpdatedAt = jotting.UpdatedAt,
    };
}
