using System.Collections.Generic;
using System.Security.Claims;
using BloodCore.AspNet;
using BloodLoop.Domain.Accounts;

namespace BloodLoop.Infrastructure.Identities
{
    interface ITokenFactory
    {
        JwtToken Generate(Account account, IEnumerable<Claim> claims);
        JwtToken Generate(RefreshToken refreshToken);
    }
}
