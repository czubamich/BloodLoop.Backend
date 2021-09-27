using BloodLoop.Domain.Accounts;
using BloodLoop.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography;
using BloodCore.Extensions;
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
                .AddRoles<Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services
                .AddScoped<IUserClaimsPrincipalFactory<Account>, AccountClaimsPrincipalFactory>();

            var sp = services.BuildServiceProvider();
            CreateRoles(sp).Wait();

            return services;
        }

        private static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            foreach (var role in Role.GetRoles())
            {
                var roleExist = await roleManager.RoleExistsAsync(role.Name);
                if (!roleExist)
                    await roleManager.CreateAsync(role);
            }
        }
    }
}
