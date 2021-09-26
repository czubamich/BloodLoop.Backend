using System;
using System.Threading;
using System.Threading.Tasks;
using BloodLoop.Domain.Accounts;

namespace BloodLoop.Infrastructure.Identities.Interfaces
{
    interface IRefreshTokenStore
    {
        Task<RefreshToken> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        Task RemoveAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default);
        Task RemoveExpired(DateTime now, CancellationToken cancellationToken = default);
        Task RemoveByAccount(AccountId accountId, CancellationToken cancellationToken = default);
    }
}
