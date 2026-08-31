using FluentValidation;

namespace App.Application.Features.Identity.Login;

public sealed class LoginValidator : AbstractValidator<LoginCommand> {
    public LoginValidator() {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
