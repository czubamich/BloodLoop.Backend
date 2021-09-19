using BloodCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO

            return services;
        }

        static IEnumerable<Assembly> GetAssemblies()
        {
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            var toLoad = referencedPaths
                .Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase))
                .Select(x => AssemblyName.GetAssemblyName(x))
                .Where(x => x.Name.Contains("Blood"))
                .ToList();

            toLoad.ForEach(x => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(x)));
            return loadedAssemblies.Where(x => x.FullName.Contains("Blood"));
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
