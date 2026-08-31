using App.Application.Abstractions.Messaging;
using App.Application.Features.Welcome;
using App.Domain.Common;

namespace App.Api.Endpoints;

public static class WelcomeEndpoints {
    public static void MapWelcomeEndpoints(this IEndpointRouteBuilder app) {
        app.MapGet(
            "/api/welcome",
            async (
                IQueryHandler<WelcomeQuery, Result<WelcomeResponse>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(
                await handler.HandleAsync(
                    new WelcomeQuery(),
                    cancellationToken
                )
            )
        )
        .WithName("Welcome")
        .WithTags("Temp")
        .WithSummary("Get welcome")
        .Produces<WelcomeResponse>();
    }
}
