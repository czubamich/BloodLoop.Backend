using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BloodLoop.Domain.Accounts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BloodLoop.Infrastructure.Identities
{
    /// https://benfoster.io/blog/customising-claims-transformation-in-aspnet-core-identity/
    /// </summary>
    public class AccountClaimsPrincipalFactory : UserClaimsPrincipalFactory<Account>
    {
        public AccountClaimsPrincipalFactory(UserManager<Account> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(Account user)
        {
            var principal = await base.CreateAsync(user);

            ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            });

            return principal;
        }
    }
}
