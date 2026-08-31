using App.Application.Features.Todos.Get;
using App.Domain.Common;
using App.Domain.Models;
using FluentAssertions;

namespace App.Application.UnitTests.Features.Todos;

public sealed class GetTodoQueryHandlerTests {
    [Fact]
    public async Task HandleAsync_ShouldReturnTodo_WhenFound() {
        // Arrange
        await using var dbContext = TestDbContextFactory.Create();
        var todo = new TodoItem { Title = "Test", Description = "Desc" };
        dbContext.Todos.Add(todo);
        await dbContext.SaveChangesAsync(TestContext.Current.CancellationToken);

        var handler = new GetTodoQueryHandler(dbContext);

        // Act
        var result = await handler.HandleAsync(new GetTodoQuery(todo.Id), TestContext.Current.CancellationToken);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value!.Title.Should().Be("Test");
    }

    [Fact]
    public async Task HandleAsync_ShouldReturnNotFound_WhenMissing() {
        // Arrange
        await using var dbContext = TestDbContextFactory.Create();
        var handler = new GetTodoQueryHandler(dbContext);

        // Act
        var result = await handler.HandleAsync(new GetTodoQuery(Guid.NewGuid()), TestContext.Current.CancellationToken);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error!.Type.Should().Be(ErrorType.NotFound);
    }
}
