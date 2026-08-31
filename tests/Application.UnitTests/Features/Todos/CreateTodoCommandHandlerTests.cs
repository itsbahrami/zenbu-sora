using System.Linq;
using System.Threading.Tasks;
using App.Application.Features.Todos.Create;
using FluentAssertions;

namespace App.Application.UnitTests.Features.Todos;

public sealed class CreateTodoCommandHandlerTests {
    [Fact]
    public async Task HandleAsync_ShouldReturnSuccess_WithCreatedTodo() {
        // Arrange
        await using var dbContext = TestDbContextFactory.Create();
        var handler = new CreateTodoCommandHandler(dbContext);
        var command = new CreateTodoCommand("Test Todo", "Test Description");

        // Act
        var result = await handler.HandleAsync(command, TestContext.Current.CancellationToken);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.Title.Should().Be("Test Todo");
        dbContext.Todos.Should().HaveCount(1);
    }

    [Fact]
    public async Task HandleAsync_ShouldPersistTitleAndDescription() {
        // Arrange
        await using var dbContext = TestDbContextFactory.Create();
        var handler = new CreateTodoCommandHandler(dbContext);
        var command = new CreateTodoCommand("My Task", "Some details");

        // Act
        await handler.HandleAsync(command, TestContext.Current.CancellationToken);

        // Assert
        var todo = dbContext.Todos.Single();
        todo.Title.Should().Be("My Task");
        todo.Description.Should().Be("Some details");
    }
}
