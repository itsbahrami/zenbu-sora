using App.Application.Abstractions.Messaging;

namespace App.Application.Features.Identity.Register;

public sealed record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    string ConfirmPassword
) : ICommand;
