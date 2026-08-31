using App.Application.Abstractions.Identity;
using App.Application.Abstractions.Messaging;
using App.Domain.Common;

namespace App.Application.Features.Identity.RefreshToken;

public sealed class RefreshTokenCommandHandler(ITokenService tokenService)
    : ICommandHandler<RefreshTokenCommand, Result<TokenResponse>> {
    public async Task<Result<TokenResponse>> HandleAsync(
        RefreshTokenCommand command,
        CancellationToken cancellationToken = default
    ) {
        try {
            var token = await tokenService.RefreshTokenAsync(
                command.AccessToken,
                command.RefreshToken,
                cancellationToken
            );

            return Result.Success(token);
        } catch (InvalidOperationException ex) {
            return Result.Failure<TokenResponse>(Error.Validation("Auth.InvalidRefreshToken", ex.Message));
        }
    }
}
