using Ardalis.GuardClauses;
using BloodCore.Domain;
using BloodCore.Extensions;
using BloodLoop.Domain.Accounts;
using BloodLoop.Domain.Donations;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Donors
{
    public class Donor : AggregateRoot<DonorId>
    {
        public AccountId AccountId { get; private set; }
        public Account Account { get; private set; }

        [ProtectedPersonalData]
        public Pesel Pesel { get; private set; }
        [ProtectedPersonalData]
        public GenderType Gender { get; private set; }
        [ProtectedPersonalData]
        public DateTime BirthDay { get; private set; }

        public List<Donation> _donations;
        public IReadOnlyCollection<Donation> Donations => _donations;

        #region Constructors

        public Donor()
        {
        }

        public Donor(DonorId id, AccountId accountId, GenderType gender, DateTime birthDay)
        {
            Id = Guard.Against.NullOrDefault(id, nameof(Id));
            AccountId = Guard.Against.NullOrDefault(accountId, nameof(AccountId));
            ChangeGender(gender);
            ChangeBirthDay(birthDay);
        }

        public Donor(DonorId id, Account account, GenderType gender, DateTime birthDay)
        {
            Id = Guard.Against.NullOrDefault(id, nameof(Id));
            SetAccount(account);
            ChangeGender(gender);
            ChangeBirthDay(birthDay);
        }

        #endregion

        #region Creations

        internal static Donor Create(DonorId id, Account account, GenderType gender, DateTime birthDay)
            => new Donor(id, account, gender, birthDay);

        internal static Donor Create(Account account, GenderType gender, DateTime birthDay)
            => new Donor(DonorId.New, account, gender, birthDay);

        internal static Donor Create(AccountId accountId, GenderType gender, DateTime birthDay)
            => new Donor(DonorId.New, accountId, gender, birthDay);

        #endregion

        #region Behaviours

        public Donor ChangeGender(GenderType gender)
        {
            Gender = Guard.Against.Null(gender, nameof(Gender));

            return this;
        }

        public Donor ChangeBirthDay(DateTime birthDay)
        {
            BirthDay = Guard.Against.InvalidInput(birthDay, nameof(BirthDay), b => birthDay.PassedLongerThan(DateTime.UtcNow, DateTime.UtcNow.DifferYears(16)));

            return this;
        }

        public Donor SetPesel(Pesel pesel)
        {
            if (Pesel is not null)
                throw new DomainException("Pesel already set");

            if (pesel.IsValid() == false)
                throw new DomainException("Provided pesel is invalid");

            Pesel = Guard.Against.Null(pesel, nameof(Pesel));

            return this;
        }

        internal Donor SetAccount(Account account)
        {
            if (Account is not null || AccountId is not null)
                throw new DomainException("Account already associated");

            Account = account;
            AccountId = account.Id;

            return this;
        }

        #endregion
    }
}
