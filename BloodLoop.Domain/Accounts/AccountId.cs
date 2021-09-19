using BloodCore.Common;
using System;

namespace BloodLoop.Domain.Accounts
{
    public class AccountId : Identity
    {
        private AccountId(Guid id) : base(id) { }

        public static AccountId Of(string id) => Of(new Guid(id));

        public static AccountId Of(Guid id) => new AccountId(id);

        public static AccountId New => new AccountId(Guid.NewGuid());
    }
}
