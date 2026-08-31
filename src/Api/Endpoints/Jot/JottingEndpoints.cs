using App.Application.Abstractions.Messaging;
using App.Application.Features.Jot.Jottings;
using App.Application.Features.Jot.Jottings.Create;
using App.Application.Features.Jot.Jottings.Delete;
using App.Application.Features.Jot.Jottings.GetAll;
using App.Application.Features.Jot.Jottings.Update;
using App.Domain.Common;

namespace App.Api.Endpoints.Jot;

public static class JottingEndpoints {
    public static void MapJottingEndpoints(this IEndpointRouteBuilder app) {
        var group = app
            .MapGroup("/api/jot/jottings")
            .WithTags("Jottings")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapGet(
            "/",
            async (
                IQueryHandler<GetAllJottingsQuery, Result<List<JottingResponse>>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(
                await handler.HandleAsync(new GetAllJottingsQuery(), cancellationToken)
            )
        )
        .WithName("GetAllJottings")
        .WithSummary("Get all jottings")
        .Produces<List<JottingResponse>>(StatusCodes.Status200OK);

        group.MapPost(
            "/",
            async (
                CreateJottingCommand command,
                ICommandHandler<CreateJottingCommand, Result<JottingResponse>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(
                await handler.HandleAsync(command, cancellationToken)
            )
        )
        .WithName("CreateJotting")
        .WithSummary("Create a new jotting")
        .Produces<JottingResponse>(StatusCodes.Status201Created)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        group.MapPut(
            "/{id:guid}",
            async (
                Guid id,
                UpdateJottingRequest request,
                ICommandHandler<UpdateJottingCommand, Result> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveNoContent(
                await handler.HandleAsync(
                    new UpdateJottingCommand(id, request.Title, request.Content),
                    cancellationToken
                )
            )
        )
        .WithName("UpdateJotting")
        .WithSummary("Update an existing jotting")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);

        group.MapDelete(
            "/{id:guid}",
            async (
                Guid id,
                ICommandHandler<DeleteJottingCommand, Result> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveNoContent(
                await handler.HandleAsync(new DeleteJottingCommand(id), cancellationToken)
            )
        )
        .WithName("DeleteJotting")
        .WithSummary("Delete a jotting")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}

public sealed record UpdateJottingRequest(string Title, string Content);
