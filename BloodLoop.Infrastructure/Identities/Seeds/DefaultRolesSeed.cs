using System.Threading.Tasks;
using BloodLoop.Domain.Accounts;
using Microsoft.AspNetCore.Identity;

namespace BloodLoop.Infrastructure.Identities.Seeds
{
    public static class DefaultRolesSeed
    {
        public static async Task SeedAsync(UserManager<Account> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Staff.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Donor.ToString()));
        }
    }
}