using App.Domain.Models.Matahang;

namespace App.Application.Features.Matahang.Aahamatns;

public sealed record AahamatnFullResponse {
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Artist { get; init; }
    public required Language Language { get; init; }
    public required string Lyrics { get; init; }
    public string? Color { get; init; }
    public string? AudioUrl { get; init; }
    public string? SourceUrl { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }

    public static AahamatnFullResponse FromDomain(Aahamatn d) => new() {
        Id = d.Id,
        Title = d.Title,
        Language = d.Language,
        Lyrics = d.Lyrics,
        Artist = d.Artist,
        AudioUrl = d.AudioUrl,
        Color = d.Color,
        SourceUrl = d.SourceUrl,
        UpdatedAt = d.UpdatedAt,
    };
}
