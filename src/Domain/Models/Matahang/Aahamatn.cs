using App.Domain.Common;

namespace App.Domain.Models.Matahang;

/// <summary>
/// The lyrics of a song, without the music itself. 🫠
/// </summary>
public sealed class Aahamatn : AggregateRoot {
    /// <summary>
    /// The song title.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// The singer or band name.
    /// </summary>
    public string? Artist { get; set; }

    /// <summary>
    /// The language of the lyrics.
    /// </summary>
    public required Language Language { get; set; }

    /// <summary>
    /// The lyrics text.
    /// </summary>
    public required string Lyrics { get; set; }

    /// <summary>
    /// An optional color, managed by the consumer.
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// A link to where the song can be listened to.
    /// </summary>
    public string? AudioUrl { get; set; }

    /// <summary>
    /// A link to the source where these lyrics were found.
    /// </summary>
    public string? SourceUrl { get; set; }
}
