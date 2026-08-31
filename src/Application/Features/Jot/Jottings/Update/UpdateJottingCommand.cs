using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Jot.Jottings.Update;

public sealed record UpdateJottingCommand(
    Guid Id,
    string Title,
    string Content
) : ICommand<Result>;
