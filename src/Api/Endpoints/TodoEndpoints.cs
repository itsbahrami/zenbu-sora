using App.Api.Extensions;
using App.Application.Abstractions.Messaging;
using App.Application.Features.Todos.Complete;
using App.Application.Features.Todos.Create;
using App.Application.Features.Todos.Delete;
using App.Application.Features.Todos.Get;
using App.Application.Features.Todos.GetAll;
using App.Application.Features.Todos.Update;
using App.Domain.Common;

namespace App.Api.Endpoints;

public static class TodoEndpoints {
    public static void MapTodoEndpoints(this IEndpointRouteBuilder app) {
        var group = app
            .MapGroup("/api/todos")
            .WithTags("Todos")
            .RequireAuthorization();

        // --------------------

        group.MapGet(
            "/",
            async (
                int? page,
                int? pageSize,
                IQueryHandler<GetAllTodosQuery, Result<PagedResult<TodoDetailResponse>>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(
                await handler.HandleAsync(
                    new GetAllTodosQuery(page ?? 1, pageSize ?? 10),
                    cancellationToken
                )
            )
        )
        .WithName("GetAllTodos")
        .WithSummary("Get all todos with pagination")
        .Produces<PagedResult<TodoDetailResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);

        // --------------------

        group.MapGet(
            "/{id:guid}",
            async (
                Guid id,
                IQueryHandler<GetTodoQuery, Result<TodoDetailResponse>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveOk(
                await handler.HandleAsync(new GetTodoQuery(id), cancellationToken)
            )
        )
        .WithName("GetTodoById")
        .WithSummary("Get a todo by ID")
        .Produces<TodoDetailResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized);

        // --------------------

        group.MapPost(
            "/",
            async (
                CreateTodoCommand command,
                ICommandHandler<CreateTodoCommand, Result<CreateTodoResponse>> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveCreatedAt(
                await handler.HandleAsync(command, cancellationToken),
                "GetTodoById",
                value => new { id = value.Id }
            )
        )
        .AddEndpointFilter<ValidationFilter<CreateTodoCommand>>()
        .WithName("CreateTodo")
        .WithSummary("Create a new todo")
        .Produces<CreateTodoResponse>(StatusCodes.Status201Created)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized);

        // --------------------

        group.MapPut(
            "/{id:guid}",
            async (
                Guid id,
                UpdateTodoRequest request,
                ICommandHandler<UpdateTodoCommand, Result> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveNoContent(
                await handler.HandleAsync(
                    new UpdateTodoCommand(id, request.Title, request.Description),
                    cancellationToken
                )
            )
        )
        .WithName("UpdateTodo")
        .WithSummary("Update an existing todo")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesValidationProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized);

        // --------------------

        group.MapPatch(
            "/{id:guid}/complete",
            async (
                Guid id,
                ICommandHandler<CompleteTodoCommand, Result> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveNoContent(
                await handler.HandleAsync(new CompleteTodoCommand(id), cancellationToken)
            )
        )
        .WithName("CompleteTodo")
        .WithSummary("Mark a todo as completed")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized);

        // --------------------

        group.MapDelete(
            "/{id:guid}",
            async (
                Guid id,
                ICommandHandler<DeleteTodoCommand, Result> handler,
                CancellationToken cancellationToken
            ) => EndpointUtils.AutoResolveNoContent(
                await handler.HandleAsync(new DeleteTodoCommand(id), cancellationToken)
            )
        )
        .WithName("DeleteTodo")
        .WithSummary("Delete a todo")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized);
    }
}

public sealed record UpdateTodoRequest(string Title, string? Description);
