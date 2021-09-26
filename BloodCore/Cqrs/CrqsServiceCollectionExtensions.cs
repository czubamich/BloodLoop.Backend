using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BloodCore.Api;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BloodCore.Cqrs
{
    public static class CrqsServiceCollectionExtensions
    {
        public static IServiceCollection AddCqrs(this IServiceCollection services, IEnumerable<Assembly> scannedAssemblies = null)
        {
            var assemblies = (scannedAssemblies ?? AppDomain.CurrentDomain.GetAssemblies()).ToArray();

            services.AddValidatorsFromAssemblies(assemblies);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddMediatR(assemblies);
            services.AddAutoMapper(assemblies);

            return services;
        }

        public static IServiceCollection Exclude(this IServiceCollection services, params Type[] types)
        {
            foreach (var type in types.AsEnumerable())
            {
                services.Remove(services.FirstOrDefault(descriptor => descriptor.ImplementationType == type));
            }

            return services;
        }
    }
}
