using BloodLoop.Domain.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BloodLoop.WebApi.Extensions
{
    public static class ClaimsPrincipalExt
    {
        public static AccountId AccountId(this ClaimsPrincipal claimsPrincipal)
            => Domain.Accounts.AccountId.Of(claimsPrincipal.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);

        public static IEnumerable<string> Roles(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.Claims.Where(c => c.Type == ClaimTypes.Role).Select(x => x.Value);
    }
}
