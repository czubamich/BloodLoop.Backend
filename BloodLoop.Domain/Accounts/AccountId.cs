using BloodCore.Common;
using System;

namespace BloodLoop.Domain.Accounts
{
    public class AccountId : Identity<AccountId>, IEquatable<AccountId>
    {
        private AccountId(Guid id) : base(id) { }

        public bool Equals(AccountId other)
        {
            if (other is null) return false;
            return Id == other.Id;
        }
    }
}
