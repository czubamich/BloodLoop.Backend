using BloodLoop.Domain.Accounts;
using BloodLoop.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodLoop.WebApi.Extensions
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            //TODO: Configure authentication and authorization

            return services;
        }
    }
}
