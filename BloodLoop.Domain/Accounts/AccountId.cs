using BloodCore.Common;
using System;

namespace BloodLoop.Domain.Accounts
{
    public class AccountId : Identity, IEquatable<AccountId>
    {
        private AccountId(Guid id) : base(id) { }

        public static AccountId Of(string id) => Of(new Guid(id));

        public static AccountId Of(Guid id) => new AccountId(id);

        public static AccountId New => new AccountId(Guid.NewGuid());

        public bool Equals(AccountId other)
        {
            if (other is null) return false;
            return Id == other.Id;
        }
    }
}
