using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using BloodCore;
using BloodLoop.Domain.Accounts;
using BloodLoop.Infrastructure.Identities;
using BloodLoop.Infrastructure.Identities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BloodLoop.Infrastructure.Persistance.Repositories
{
    [Injectable]
    class RefreshTokenStore : IRefreshTokenStore
    {
        private readonly ApplicationDbContext _dbContext;

        public RefreshTokenStore(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RefreshToken> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.RefreshTokens.FindAsync(id, cancellationToken);
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        {
            _dbContext.RefreshTokens.Remove(refreshToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveExpired(DateTime now, CancellationToken cancellationToken = default)
        {
            var expiredTokens = await _dbContext.RefreshTokens.Where(x => x.IsExpired).ToListAsync(cancellationToken);

            _dbContext.RefreshTokens.RemoveRange(expiredTokens);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveByAccount(AccountId accountId, CancellationToken cancellationToken = default)
        {
            var accountsTokens = await _dbContext.RefreshTokens.Where(x => x.AccountId == accountId).ToListAsync(cancellationToken);

            _dbContext.RefreshTokens.RemoveRange(accountsTokens);
            await _dbContext.SaveChangesAsync();
        }
    }
}
