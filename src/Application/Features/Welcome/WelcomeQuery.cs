using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Welcome;

public sealed record WelcomeQuery : IQuery<Result<WelcomeResponse>>;
