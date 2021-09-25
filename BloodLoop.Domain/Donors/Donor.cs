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
        public AccountId AccountId { get; init; }
        public Account Account { get; }

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
        
        public Donor(DonorId id, AccountId accountId, Pesel pesel, GenderType gender, DateTime birthDay) : base(id)
        {
            if (pesel is null || Pesel.IsValid() == false)
                throw new DomainException($"Provided {nameof(Pesel)} is invalid");

            AccountId = Guard.Against.NullOrDefault(accountId, nameof(AccountId));
            Pesel = pesel;
            ChangeGender(gender);
            ChangeBirthDay(birthDay);
        }


        #endregion

        #region Creations

        internal static Donor Create(DonorId id, AccountId accountId, Pesel pesel, GenderType gender, DateTime birthDay)
            => new Donor(id, accountId, pesel, gender, birthDay);

        internal static Donor Create(AccountId accountId, Pesel pesel, GenderType gender, DateTime birthDay)
            => new Donor(DonorId.New, accountId, pesel, gender, birthDay);

        #endregion

        #region Behaviours

        public Donor ChangeGender(GenderType gender)
        {
            Gender = Guard.Against.Null(gender, nameof(Gender));

            return this;
        }

        public Donor ChangeBirthDay(DateTime birthDay)
        {
            BirthDay = Guard.Against.InvalidInput(birthDay, nameof(BirthDay), b => birthDay.PassedLongerThan(DateTime.Now, DateTime.Now.DifferYears(-16)));

            return this;
        }
         
        #endregion
    }
}
