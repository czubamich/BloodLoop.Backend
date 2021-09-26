﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using BloodCore;
using BloodCore.AspNet;
using BloodLoop.Api.Responses;
using BloodLoop.Application.Services;
using BloodLoop.Domain.Accounts;
using BloodLoop.Infrastructure.Identities.Interfaces;
using BloodLoop.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BloodLoop.Infrastructure.Identities
{
    [Injectable]
    class IdentityService : IIdentityService
    {
        private readonly UserManager<Account> _userManager;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly ITokenFactory _tokenFactory;

        private const string FAIL_MESSAGE = "Invalid username/e-mail or password";
        private const string EMAIL_MESSAGE = "Email confirmation is required";

        public IdentityService(
            UserManager<Account> userManager,
            IOptions<AuthenticationSettings> authenticationSettings,
            IRefreshTokenStore refreshTokenStore,
            ITokenFactory tokenFactory)
        {
            _userManager = userManager;
            _authenticationSettings = authenticationSettings?.Value ?? throw new ArgumentNullException(nameof(authenticationSettings));
            _tokenValidationParameters = _authenticationSettings.CreateTokenValidationParameters();
            _refreshTokenStore = refreshTokenStore;
            _tokenFactory = tokenFactory;
        }

        public async Task<AuthenticationResult> SignInAsync(string userNameOrEmail, string password, CancellationToken cancellationToken)
        {
            var account = await _userManager.FindByEmailAsync(userNameOrEmail) 
                          ?? await _userManager.FindByNameAsync(userNameOrEmail);

            if (account is null || !await _userManager.CheckPasswordAsync(account, password))
                return AuthenticationResult.Failed(FAIL_MESSAGE);

            if (_userManager.Options.SignIn.RequireConfirmedEmail && !await _userManager.IsEmailConfirmedAsync(account))
                return AuthenticationResult.Failed(EMAIL_MESSAGE);

            return await AuthenticateAsync(account, cancellationToken);
        }

        private async Task<AuthenticationResult> AuthenticateAsync(Account account, CancellationToken cancellationToken)
        {
            JwtToken jwtAccessToken = _tokenFactory.Generate(account, await _userManager.GetClaimsAsync(account));

            var refreshTokenLifeTime = TimeSpan.FromMinutes(_authenticationSettings.RefreshTokenLifetimeMinutes);
            RefreshToken refreshToken = new RefreshToken(account.Id, refreshTokenLifeTime);

            JwtToken jwtRefreshToken = _tokenFactory.Generate(refreshToken);

            await _refreshTokenStore.AddAsync(refreshToken, cancellationToken);

            return AuthenticationResult.Succeed(null, jwtAccessToken, jwtRefreshToken);
        }

        public async Task<AuthenticationResult> RefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            var principalFromToken = GetPrincipalFromToken(refreshToken);

            if (principalFromToken is null)
                throw new AuthenticationException("Invalid refresh token");

            Guid refreshTokenId = Guid.Parse(principalFromToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

            RefreshToken storedRefreshToken = await _refreshTokenStore.GetByIdAsync(refreshTokenId, cancellationToken);

            if (storedRefreshToken is null)
                throw new AuthenticationException("Refresh token does not exist");

            AccountId accountId = AccountId.Of(principalFromToken.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (storedRefreshToken.AccountId != accountId)
                throw new AuthenticationException("Invalid refresh token");

            Account account = await _userManager.GetUserAsync(principalFromToken);

            await _refreshTokenStore.RemoveAsync(storedRefreshToken, cancellationToken);
            return await AuthenticateAsync(account, cancellationToken);
        }

        public async Task RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken)
        {
            var principalFromToken = GetPrincipalFromToken(refreshToken);

            if (principalFromToken is null)
                throw new AuthenticationException("Invalid refresh token");

            Guid refreshTokenId = Guid.Parse(principalFromToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value);

            RefreshToken storedRefreshToken = await _refreshTokenStore.GetByIdAsync(refreshTokenId, cancellationToken);

            if (storedRefreshToken is null)
                throw new AuthenticationException("This refresh token does not exist");

            AccountId accountId = AccountId.Of(principalFromToken.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value);

            if (storedRefreshToken.AccountId != accountId)
                throw new AuthenticationException("Invalid refresh token");

            await _refreshTokenStore.RemoveAsync(storedRefreshToken, cancellationToken);
        }

        public async Task SignOutAsync(AccountId accountId, CancellationToken cancellationToken)
        {
            await _refreshTokenStore.RemoveByAccount(accountId, cancellationToken);
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                       StringComparison.InvariantCultureIgnoreCase);
        }
    }
}