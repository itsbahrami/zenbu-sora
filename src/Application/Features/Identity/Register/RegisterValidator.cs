using FluentValidation;

namespace App.Application.Features.Identity.Register;

public sealed class RegisterValidator : AbstractValidator<RegisterCommand> {
    public RegisterValidator() {
        RuleFor(x => x.DisplayName)
            .MaximumLength(100);

        RuleFor(x => x.FullName)
            .MaximumLength(100);

        RuleFor(x => x.UserName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password)
            .WithMessage("Passwords do not match.");
    }
}
