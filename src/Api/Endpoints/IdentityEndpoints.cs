using App.Api.Extensions;
using App.Application.Abstractions.Identity;
using App.Application.Abstractions.Messaging;
using App.Application.Features.Identity.Login;
using App.Application.Features.Identity.RefreshToken;
using App.Application.Features.Identity.Register;
using App.Domain.Common;

namespace App.Api.Endpoints;

public static class IdentityEndpoints {
    public static void MapIdentityEndpoints(this IEndpointRouteBuilder app) {
        var group = app
            .MapGroup("/api/identity")
            .WithTags("Identity");

        // --------------------

        group.MapPost(
            "/register",
            async (
                RegisterCommand command,
                ICommandHandler<RegisterCommand, Result> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(await handler.HandleAsync(command, cancellationToken))
        )
        .AddEndpointFilter<ValidationFilter<RegisterCommand>>()
        .WithName("Register")
        .WithSummary("Register a new user")
        .Produces(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        // --------------------

        group.MapPost(
            "/login",
            async (
                LoginCommand command,
                ICommandHandler<LoginCommand, Result<TokenResponse>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(await handler.HandleAsync(command, cancellationToken))
        )
        .AddEndpointFilter<ValidationFilter<LoginCommand>>()
        .WithName("Login")
        .WithSummary("Login with email and password")
        .Produces<TokenResponse>(StatusCodes.Status200OK)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);

        // --------------------

        group.MapPost(
            "/refresh",
            async (
                RefreshTokenCommand command,
                ICommandHandler<RefreshTokenCommand, Result<TokenResponse>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(await handler.HandleAsync(command, cancellationToken))
        )
        .WithName("RefreshToken")
        .WithSummary("Refresh an expired access token")
        .Produces<TokenResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}
