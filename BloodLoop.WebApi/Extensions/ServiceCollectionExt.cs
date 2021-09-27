using BloodLoop.Domain.Accounts;
using BloodLoop.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloodLoop.Infrastructure.Identities;

namespace BloodLoop.WebApi.Extensions
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<Account>(
                    opt =>
                    {
                        opt.SignIn.RequireConfirmedEmail = false;
                        opt.User.RequireUniqueEmail = true;

                        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                        opt.Lockout.MaxFailedAccessAttempts = 3;

                        opt.Password.RequireDigit = true;
                        opt.Password.RequireLowercase = true;
                        opt.Password.RequireUppercase = true;
                        opt.Password.RequireNonAlphanumeric = true;
                    })
                .AddRoles<IdentityRole<AccountId>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddScoped<IUserClaimsPrincipalFactory<Account>, AccountClaimsPrincipalFactory>();

            return services;
        }
    }
}
