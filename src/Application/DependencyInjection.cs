using System.Reflection;
using App.Application.Abstractions.Messaging;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application;

public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddValidatorsFromAssembly(assembly);
        services.AddHandlersFromAssembly(assembly);

        return services;
    }

    private static void AddHandlersFromAssembly(this IServiceCollection services, Assembly assembly) {
        Type[] handlerInterfaceTypes = [
            typeof(ICommandHandler<,>),
            typeof(IQueryHandler<,>)
        ];

        var types = assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .ToList();

        foreach (var type in types) {
            var interfaces = type.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            handlerInterfaceTypes.Contains(i.GetGenericTypeDefinition()
                ));

            foreach (var handlerInterface in interfaces) {
                services.AddScoped(handlerInterface, type);
            }
        }
    }
}
