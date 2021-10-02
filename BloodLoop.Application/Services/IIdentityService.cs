using System.Threading;
using System.Threading.Tasks;
using BloodLoop.Application.Auth.Responses;
using BloodLoop.Domain.Accounts;

namespace BloodLoop.Application.Services
{
    public interface IIdentityService
    {
        public Task<AuthenticationResult> SignInAsync(string userNameOrEmail, string password, CancellationToken cancellationToken = default);
        public Task<AuthenticationResult> RefreshToken(string refreshToken, CancellationToken cancellationToken = default);
        public Task SignOutAsync(AccountId accountId, string refreshToken, CancellationToken cancellationToken = default);
        public Task RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken = default);
    }
}
