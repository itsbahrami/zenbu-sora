using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using App.Domain.Models.Matahang;

namespace App.Application.Features.Matahang.Aahamatns.Update;

public sealed record UpdateAahamatnCommand(
    Guid Id,
    string Title,
    Language Language,
    string Lyrics,
    string? Artist,
    string? Color,
    string? AudioUrl,
    string? SourceUrl
) : ICommand<Result>;
