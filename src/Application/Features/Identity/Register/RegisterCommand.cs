using App.Application.Abstractions.Messaging;

namespace App.Application.Features.Identity.Register;

public sealed record RegisterCommand(
    string? FullName,
    string? DisplayName,
    string UserName,
    string Email,
    string Password,
    string ConfirmPassword
) : ICommand;
