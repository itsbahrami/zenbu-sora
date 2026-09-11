using App.Application.Abstractions.Messaging;
using App.Application.Features.Matahang.Aahamatns;
using App.Application.Features.Matahang.Aahamatns.Create;
using App.Application.Features.Matahang.Aahamatns.Delete;
using App.Application.Features.Matahang.Aahamatns.Get;
using App.Application.Features.Matahang.Aahamatns.GetAll;
using App.Application.Features.Matahang.Aahamatns.Update;
using App.Domain.Common;

namespace App.Api.Endpoints.Matahang;

public static class AahamatnEndpoints {
    public static void MapAahamatnEndpoints(this IEndpointRouteBuilder app) {
        var group = app
            .MapGroup("/api/matahang/aahamatns")
            .WithTags("Aahamatns")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapGet(
            "/",
            async (
                IQueryHandler<GetAllAahamatnsQuery, Result<AahamatnsResponse>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(
                await handler.HandleAsync(new GetAllAahamatnsQuery(), cancellationToken)
            )
        )
        .WithName("GetAllAahamatns")
        .WithSummary("Get all Aahamatns")
        .Produces<AahamatnsResponse>(StatusCodes.Status200OK);

        group.MapGet(
            "/{id:guid}",
            async (
                Guid id,
                IQueryHandler<GetAahamatnQuery, Result<AahamatnFullResponse>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(
                await handler.HandleAsync(new GetAahamatnQuery(id), cancellationToken)
            )
        )
        .WithName("GetAahamatn")
        .WithSummary("Get an Aahamatn")
        .Produces<AahamatnFullResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPost(
            "/",
            async (
                CreateAahamatnCommand command,
                ICommandHandler<CreateAahamatnCommand, Result<AahamatnFullResponse>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(
                await handler.HandleAsync(command, cancellationToken)
            )
        )
        .WithName("CreateAahamatn")
        .WithSummary("Create a new Aahamatn")
        .Produces<AahamatnFullResponse>(StatusCodes.Status201Created)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest);

        group.MapPut(
            "/{id:guid}",
            async (
                Guid id,
                UpdateAahamatnCommand request,
                ICommandHandler<UpdateAahamatnCommand, Result> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveNoContent(
                await handler.HandleAsync(
                    request with { Id = id },
                    cancellationToken
                )
            )
        )
        .WithName("UpdateAahamatn")
        .WithSummary("Update an existing Aahamatn")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound);

        group.MapDelete(
            "/{id:guid}",
            async (
                Guid id,
                ICommandHandler<DeleteAahamatnCommand, Result> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveNoContent(
                await handler.HandleAsync(new DeleteAahamatnCommand(id), cancellationToken)
            )
        )
        .WithName("DeleteAahamatn")
        .WithSummary("Delete an Aahamatn")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
