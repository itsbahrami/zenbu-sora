using App.Application.Features.Todos.Delete;
using App.Domain.Common;
using App.Domain.Models;
using FluentAssertions;

namespace App.Application.UnitTests.Features.Todos;

public sealed class DeleteTodoCommandHandlerTests {
    [Fact]
    public async Task HandleAsync_ShouldDeleteTodo_WhenFound() {
        // Arrange
        await using var dbContext = TestDbContextFactory.Create();
        var todo = new TodoItem { Title = "Test" };
        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var handler = new DeleteTodoCommandHandler(dbContext);

        // Act
        var result = await handler.HandleAsync(new DeleteTodoCommand(todo.Id), TestContext.Current.CancellationToken);

        // Assert
        result.IsSuccess.Should().BeTrue();
        dbContext.Todos.Should().BeEmpty();
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnNotFound_WhenMissing() {
        // Arrange
        await using var dbContext = TestDbContextFactory.Create();
        var handler = new DeleteTodoCommandHandler(dbContext);

        // Act
        var result = await handler.HandleAsync(
            new DeleteTodoCommand(Guid.NewGuid()),
            TestContext.Current.CancellationToken
        );

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error!.Type.Should().Be(ErrorType.NotFound);
    }
}
