using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using UniversityScheduler.Api.Core.Enums;
using UniversityScheduler.Api.Core.Models.Attributes;

namespace UniversityScheduler.Api.Core.Extensions;

public static class ServiceCollectionExtension
{
    public static void InjectServices(this IServiceCollection services, string @namespace)
    {
        var assemblyToLoad = Assembly.Load(@namespace);

        var typesToInject = assemblyToLoad
            .GetTypes()
            .Where(type => type.GetCustomAttributes().OfType<RegistrationAttribute>().Any());

        foreach (var typeToInject in typesToInject)
        {
            var interfaceTypeToInject = typeToInject.GetInterfaces().First();

            var registrationAttribute =
                typeToInject.GetCustomAttribute(typeof(RegistrationAttribute)) as RegistrationAttribute;

            switch (registrationAttribute?.Type)
            {
                case RegistrationKind.Scoped:
                {
                    services.AddScoped(interfaceTypeToInject, typeToInject);
                    break;
                }

                case RegistrationKind.Singleton:
                {
                    services.AddSingleton(interfaceTypeToInject, typeToInject);
                    break;
                }

                case RegistrationKind.Transient:
                {
                    services.AddTransient(interfaceTypeToInject, typeToInject);
                    break;
                }

                default:
                    return;
            }
        }
    }

    public static void InjectServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var typesToInject = assembly.GetTypes().Where(type =>
            type.GetCustomAttributes().OfType<RegistrationAttribute>().Any());

        foreach (var typeToInject in typesToInject)
        {
            var interfaceTypeToInject = typeToInject.GetInterfaces().First();

            var registrationAttribute =
                typeToInject.GetCustomAttribute(typeof(RegistrationAttribute)) as RegistrationAttribute;

            switch (registrationAttribute?.Type)
            {
                case RegistrationKind.Scoped:
                {
                    services.AddScoped(interfaceTypeToInject, typeToInject);
                    break;
                }

                case RegistrationKind.Singleton:
                {
                    services.AddSingleton(interfaceTypeToInject, typeToInject);
                    break;
                }


                case RegistrationKind.Transient:
                {
                    services.AddTransient(interfaceTypeToInject, typeToInject);
                    break;
                }

                default:
                    return;
            }
        }
    }
}