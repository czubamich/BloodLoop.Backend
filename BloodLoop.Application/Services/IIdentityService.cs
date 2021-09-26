using System.Threading;
using System.Threading.Tasks;
using BloodLoop.Api.Responses;
using BloodLoop.Domain.Accounts;

namespace BloodLoop.Application.Services
{
    public interface IIdentityService
    {
        public Task<AuthenticationResult> SignInAsync(string userNameOrEmail, string password, CancellationToken cancellationToken = default);
        public Task SignOutAsync(AccountId accountId, CancellationToken cancellationToken = default);
        public Task<AuthenticationResult> RefreshToken(string refreshToken, CancellationToken cancellationToken = default);
        public Task RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken = default);
    }
}
