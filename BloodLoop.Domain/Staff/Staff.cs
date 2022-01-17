using Ardalis.GuardClauses;
using BloodCore.Domain;
using BloodCore.Extensions;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.BloodBanks;

namespace BloodLoop.Domain.Staff
{
    public class Staff : AggregateRoot<StaffId>
    {
        public AccountId AccountId { get; private set; }
        public virtual Account Account { get; private set; }

        public BloodBankId BloodBankId { get; private set; }
        public virtual BloodBank BloodBank { get; private set; }

        #region Constructors

        private Staff()
        {
        }

        internal Staff(StaffId id, AccountId accountId, BloodBankId bloodBankId)
        {
            Id = Guard.Against.NullOrDefault(id, nameof(Id));
            AccountId = Guard.Against.NullOrDefault(accountId, nameof(AccountId));
            BloodBankId = Guard.Against.NullOrDefault(bloodBankId, nameof(AccountId));
        }
        #endregion

        #region Creations

        public static Staff Create(StaffId id, Account account, BloodBank bloodBank)
            => new(id, account.Id, bloodBank.Id);

        public static Staff Create(Account account, BloodBank bloodBank)
            => new(StaffId.New, account.Id, bloodBank.Id);
        #endregion
    }
}
