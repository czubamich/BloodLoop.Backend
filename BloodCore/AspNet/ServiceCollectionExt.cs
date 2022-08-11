using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using BloodCore.Workers;
using Microsoft.Extensions.Configuration;

namespace BloodCore.AspNet
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddSpecification(this IServiceCollection services)
        {
            services.AddScoped<ISpecificationEvaluator, SpecificationEvaluator>();

            return services;
        }

        public static IServiceCollection RegisterInjectables(this IServiceCollection services, IEnumerable<Type> types)
        {
            var definitions = types
                .Select(x => new
                {
                    InjectableAttribute = x.GetCustomAttributes(typeof(InjectableAttribute), true).OfType<InjectableAttribute>().FirstOrDefault(),
                    Type = x
                })
                .Where(x => x.InjectableAttribute != null)
                .Select(x => new
                {
                    x.InjectableAttribute.IsScoped,
                    x.Type,
                    Contracts = x.InjectableAttribute.Contracts ?? x.Type.GetInterfaces()
                })
                .SelectMany(x => x.Contracts.Select(z => new
                {
                    x.IsScoped,
                    Contract = z,
                    Implementation = x.Type
                }))
                .ToLookup(x => new { x.Implementation, x.IsScoped }, x => x.Contract);

            foreach (var registrations in definitions)
            {
                if (registrations.Key.Implementation.IsGenericTypeDefinition)
                {
                    foreach (var registration in registrations.Where(x => x != registrations.Key.Implementation && x.IsGenericType))
                    {
                        var contract = registration.GetGenericTypeDefinition();
                        if (registrations.Key.IsScoped)
                            services.AddScoped(contract, registrations.Key.Implementation);
                        else
                            services.AddTransient(contract, registrations.Key.Implementation);
                    }
                }
                else
                {
                    var implementationType = registrations.Key.Implementation;

                    if (registrations.Key.IsScoped)
                    {
                        services
                            .AddScoped(implementationType);
                    }
                    else
                    {
                        services
                            .AddTransient(implementationType);
                    }
                    foreach (var contract in registrations.Where(x => x != registrations.Key.Implementation))
                    {
                        if (registrations.Key.IsScoped)
                        {
                            services
                                .AddScoped(contract, x => x.GetService(registrations.Key.Implementation));
                        }
                        else
                        {
                            services
                                .AddTransient(contract, x => x.GetService(registrations.Key.Implementation));
                        }

                    }
                }
            }

            return services;
        }
    }
}
