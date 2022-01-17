using BloodLoop.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BloodLoop.Domain.BloodBanks;

namespace BloodLoop.WebApi.Extensions
{
    public static class ClaimsPrincipalExt
    {
        public static AccountId AccountId(this ClaimsPrincipal claimsPrincipal)
            => Domain.Accounts.AccountId.Of(claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public static BloodBankId BloodBank(this ClaimsPrincipal claimsPrincipal)
        {
            var bloodBankIdClaim = claimsPrincipal.Claims.SingleOrDefault(c => c.Type == nameof(BloodBankId))?.Value;
            return bloodBankIdClaim is not null ? Domain.BloodBanks.BloodBankId.Of(bloodBankIdClaim) : null;
        }

        public static IEnumerable<Role> Roles(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => new Role(x.Value));
    }
}
