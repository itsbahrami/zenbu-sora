using App.Domain.Models.Matahang;

namespace App.Application.Features.Matahang.Aahamatns;

public sealed record AahamatnMinimalResponse {
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Artist { get; init; }
    public required Language Language { get; init; }
    public string? Color { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }

    public static AahamatnMinimalResponse FromDomain(Aahamatn d) => new() {
        Id = d.Id,
        Title = d.Title,
        Language = d.Language,
        Artist = d.Artist,
        Color = d.Color,
        UpdatedAt = d.UpdatedAt,
    };
}
