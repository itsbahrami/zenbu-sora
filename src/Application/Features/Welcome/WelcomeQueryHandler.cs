using App.Application.Abstractions.Messaging;
using App.Application.I18n;
using App.Domain.Common;
using Microsoft.Extensions.Localization;

namespace App.Application.Features.Welcome;

public sealed class WelcomeQueryHandler(
    IStringLocalizer<SharedResources> localizer
) : IQueryHandler<WelcomeQuery, Result<WelcomeResponse>> {
    public async Task<Result<WelcomeResponse>> HandleAsync(
        WelcomeQuery query,
        CancellationToken cancellationToken = default
    ) {
        var response = new WelcomeResponse {
            Message = localizer.GetString("WelcomeMessage"),
        };

        return Result.Success(response);
    }
}
