using System;
using BloodLoop.Domain.Accounts;

namespace BloodLoop.Infrastructure.Identities
{
    public class RefreshToken
    {
        public Guid Id { get; }
        public AccountId AccountId { get; }
        public DateTime Created { get; }
        public DateTime ExpireAt { get; }
        public bool IsExpired => DateTime.UtcNow >= ExpireAt;

        private RefreshToken()
        {
        }

        public RefreshToken(AccountId accountId, TimeSpan lifeTime)
        {
            Id = Guid.NewGuid();
            AccountId = accountId ?? throw new ArgumentNullException(nameof(accountId));
            Created = DateTime.UtcNow;
            ExpireAt = Created.Add(lifeTime);
        }
    }
}
