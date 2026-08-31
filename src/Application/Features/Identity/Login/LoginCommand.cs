using App.Application.Abstractions.Identity;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Identity.Login;

public sealed record LoginCommand(string Email, string Password) : ICommand<Result<TokenResponse>>;
