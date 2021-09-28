using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BloodCore;
using BloodCore.AspNet;
using BloodLoop.Domain.Accounts;
using BloodLoop.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BloodLoop.Infrastructure.Identities
{
    [Injectable]
    internal class TokenFactory : ITokenFactory
    {
        private readonly AuthenticationSettings _authenticationOptions;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly SigningCredentials _signingCredentials;

        public TokenFactory(IOptions<AuthenticationSettings> authenticationSettings)
        {
            _authenticationOptions = authenticationSettings?.Value ?? throw new ArgumentNullException(nameof(authenticationSettings));
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationOptions.SecretKey));
            _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        public JwtToken Generate(Account account, IEnumerable<Claim> claims)
        {
            DateTime now = DateTime.UtcNow;

            ClaimsIdentity Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, account.UserName),
            });

            Subject.AddClaims(claims);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = Subject,
                Issuer = _authenticationOptions.Issuer,
                IssuedAt = now,
                Audience = _authenticationOptions.Audience,
                NotBefore = now,
                Expires = now.AddMinutes(_authenticationOptions.AccessTokenLifetimeMinutes),
                SigningCredentials = _signingCredentials,
            };

            SecurityToken securityToken = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return new JwtToken(_jwtSecurityTokenHandler.WriteToken(securityToken), _authenticationOptions.AccessTokenLifetimeMinutes);
        }

        public JwtToken Generate(RefreshToken refreshToken)
        {
            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, refreshToken.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, refreshToken.AccountId.ToString())
            };

            JwtPayload payload = new JwtPayload(_authenticationOptions.Issuer, _authenticationOptions.Audience, claims,
                refreshToken.Created, refreshToken.ExpireAt, refreshToken.Created);

            JwtSecurityToken securityToken = new JwtSecurityToken(new JwtHeader(_signingCredentials), payload);

            return new JwtToken(_jwtSecurityTokenHandler.WriteToken(securityToken), _authenticationOptions.RefreshTokenLifetimeMinutes);
        }
    }
}
