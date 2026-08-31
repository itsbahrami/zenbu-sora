using App.Application.Abstractions.Messaging;
using App.Domain.Common;
using App.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace App.Application.Features.Identity.Register;

public sealed class RegisterCommandHandler(
    UserManager<ApplicationUser> userManager
) : ICommandHandler<RegisterCommand> {
    public async Task<Result> HandleAsync(RegisterCommand command, CancellationToken cancellationToken = default) {
        var existingUserEmail = await userManager.FindByEmailAsync(command.Email);
        if (existingUserEmail is not null)
            return Result.Failure(Error.Conflict("Auth.EmailTaken", "کاربری با این ایمیل وجود داره. یه ایمیل دیگه انتخاب کنین."));

        var existingUsername = await userManager.FindByNameAsync(command.UserName);
        if (existingUsername is not null)
            return Result.Failure(Error.Conflict("Auth.UsernameTaken", "کاربری با این نام کاربری وجود داره. یه نام کاربری دیگه انتخاب کنین."));

        var user = new ApplicationUser {
            FullName = command.FullName,
            DisplayName = command.DisplayName,
            UserName = command.UserName,
            Email = command.Email,
        };

        var result = await userManager.CreateAsync(user, command.Password);
        if (!result.Succeeded) {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result.Failure(Error.Validation("Auth.RegistrationFailed", errors));
        }

        return Result.Success();
    }
}
