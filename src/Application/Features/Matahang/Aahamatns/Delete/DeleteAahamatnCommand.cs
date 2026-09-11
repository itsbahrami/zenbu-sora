using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Matahang.Aahamatns.Delete;

public sealed record DeleteAahamatnCommand(Guid Id) : ICommand<Result>;
