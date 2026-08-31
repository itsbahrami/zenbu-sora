using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Jot.Jottings.Create;

public sealed record CreateJottingCommand(string Title, string Content) : ICommand<Result<JottingResponse>>;
