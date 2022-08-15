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
using Microsoft.VisualBasic.CompilerServices;

namespace BloodLoop.Domain.Donors
{
    public class Donor : AggregateRoot<DonorId>, IAccount
    {
        public AccountId AccountId { get; private set; }
        public virtual Account Account { get; private set; }

        public Pesel Pesel { get; private set; }
        public GenderType Gender { get; private set; }
        public DateTime BirthDay { get; private set; }
        public virtual BloodType BloodType { get; private set; }

        public List<Donation> _donations;
        public virtual IReadOnlyCollection<Donation> Donations => _donations;

        #region Constructors

        public Donor()
        {
        }

        public Donor(DonorId id, Account account, GenderType gender, DateTime birthDay)
        {
            Id = Guard.Against.NullOrDefault(id, nameof(Id));
            SetAccount(account);
            ChangeGender(gender);
            ChangeBirthDay(birthDay);

            Publish(new AccountCreatedEvent(AccountId));
        }

        #endregion

        #region Creations

        internal static Donor Create(DonorId id, Account account, GenderType gender, DateTime birthDay)
            => new(id, account, gender, birthDay);

        internal static Donor Create(Account account, GenderType gender, DateTime birthDay)
            => new(DonorId.New, account, gender, birthDay);

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

        public Donor SetBloodType(BloodType bloodType)
        {
            if(BloodType != bloodType)
                BloodType = Guard.Against.Null(bloodType, nameof(BloodType));

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

        public Donor AddDonation(Donation donation, out bool isNew)
        {
            var _donation = Donation.Create(this.Id, donation.DonationTypeLabel, donation.Date);

            if (!string.IsNullOrWhiteSpace(donation.Location))
                _donation.ChangeLocation(donation.Location);

            _donation.ChangeVolume(donation.Volume > 0 ? donation.Volume 
                : _donation.DonationType?.DefaultVolume 
                ?? DonationType.GetDonationTypes().FirstOrDefault(x => x.Label == donation.DonationTypeLabel).DefaultVolume);

            var sameDayDonation = _donations.FirstOrDefault(x => x.Date.Date.Equals(donation.Date.Date));
            
            isNew = sameDayDonation is null;
            
            if (!isNew)
                _donations.Remove(sameDayDonation);
                        
            _donations.Add(_donation);

            return this;
        }

        public Donor RemoveDonation(DateTime date)
        {
            var donationToDelete = _donations.FirstOrDefault(x => x.Date == date);

            if (donationToDelete is not null)
                _donations.Remove(donationToDelete);

            return this;
        }

        #endregion
    }
}
