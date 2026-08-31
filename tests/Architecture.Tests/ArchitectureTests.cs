using System.Reflection;
using App.Application;
using App.Domain.Common;
using FluentAssertions;
using NetArchTest.Rules;

namespace App.Architecture.Tests;

public sealed class ArchitectureTests {
    private const string ApplicationNamespace = "App.Application";
    private const string InfrastructureNamespace = "App.Infrastructure";
    private const string ApiNamespace = "App.Api";
    private static readonly Assembly DomainAssembly = typeof(Entity).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(DependencyInjection).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.DependencyInjection).Assembly;

    /// <summary>
    /// The Domain layer must remain independent of the Application layer.
    /// Domain models and business rules should not reference application-specific concerns.
    /// </summary>
    [Fact]
    public void Domain_Should_Not_Depend_On_Application() {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApplicationNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// The Domain layer must remain independent of the Infrastructure layer.
    /// Domain code should not depend on databases, file systems, external services,
    /// frameworks, or implementation details.
    /// </summary>
    [Fact]
    public void Domain_Should_Not_Depend_On_Infrastructure() {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// The Domain layer must remain independent of the API layer.
    /// Domain code should not reference controllers, endpoints, DTOs,
    /// HTTP abstractions, or presentation concerns.
    /// </summary>
    [Fact]
    public void Domain_Should_Not_Depend_On_Api() {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// The Application layer must remain independent of the Infrastructure layer.
    /// Application use cases should depend only on abstractions, not concrete implementations.
    /// </summary>
    [Fact]
    public void Application_Should_Not_Depend_On_Infrastructure() {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(InfrastructureNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// The Application layer must remain independent of the API layer.
    /// Use cases, commands, queries, and handlers should not depend on presentation concerns.
    /// </summary>
    [Fact]
    public void Application_Should_Not_Depend_On_Api() {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// The Infrastructure layer must remain independent of the API layer.
    /// Infrastructure components should not reference controllers, endpoints,
    /// or other presentation-layer types.
    /// </summary>
    [Fact]
    public void Infrastructure_Should_Not_Depend_On_Api() {
        var result = Types.InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Request handlers should be sealed.
    /// Handlers are intended to be concrete implementations and should not
    /// participate in inheritance hierarchies.
    /// </summary>
    [Fact]
    public void RequestHandlers_Should_Be_Sealed() {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Validators should be sealed.
    /// Validators are implementation details and should not be extended through inheritance.
    /// </summary>
    [Fact]
    public void Validators_Should_Be_Sealed() {
        var result = Types.InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Validator")
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }

    /// <summary>
    /// Domain Models should be sealed.
    /// Prevents inheritance-based complexity and
    /// encourages explicit composition within the domain model.
    /// </summary>
    [Fact]
    public void Domain_Models_Should_Be_Sealed() {
        var result = Types.InAssembly(DomainAssembly)
            .That()
            .ResideInNamespace("App.Domain.Models")
            .Should()
            .BeSealed()
            .GetResult();

        result.IsSuccessful.Should().BeTrue();
    }
}
