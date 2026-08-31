using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Jot.Jottings.Delete;

public sealed record DeleteJottingCommand(Guid Id) : ICommand<Result>;
