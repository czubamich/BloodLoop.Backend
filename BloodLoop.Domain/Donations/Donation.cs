﻿using Ardalis.GuardClauses;
using BloodCore.Domain;
using BloodLoop.Domain.BloodBanks;
using BloodLoop.Domain.Donors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.Donations
{
    public class Donation : Entity<DonationId>
    {
        public DateTime Date { get; private set; }
        public string Location { get; private set; }
        public int Volume { get; private set; }

        public DonorId DonorId { get; private set; }
        public BloodBankId SourceBankId { get; private set;}

        public string DonationTypeLabel { get; private set; }
        public virtual DonationType DonationType { get; private set; }

        #region Constructors

        private Donation() {}

        private Donation(DonationId id, DonorId donorId, string donationTypeLabel, DateTime date) : base(id)
        {
            DonorId = Guard.Against.Null(donorId, nameof(Id));
            DonationTypeLabel = Guard.Against.NullOrWhiteSpace(donationTypeLabel, nameof(DonationTypeLabel));
            Date = Guard.Against.OutOfSQLDateRange(date, nameof(Date));
        }

        #endregion

        #region Creations

        public static Donation Create(DonationId id, DonorId donorId, string donationTypeLabel, DateTime date)
            => new Donation(id, donorId, donationTypeLabel, date);

        public static Donation Create(DonorId donorId, string donationTypeLabel, DateTime date)
            => new Donation(DonationId.New, donorId, donationTypeLabel, date);

        #endregion

        #region Behaviours

        public Donation ChangeBloodBank(BloodBankId bloodBankId)
        {
            SourceBankId = bloodBankId;

            return this;
        }
        public Donation ChangeDate(DateTime date)
        {
            Date = Guard.Against.OutOfSQLDateRange(date, nameof(Date));

            return this;
        }

        public Donation ChangeLocation(string location)
        {
            Location = Guard.Against.Null(location, nameof(Location));

            return this;
        }

        public Donation ChangeVolume(int volume)
        {
            Volume = Guard.Against.Negative(volume, nameof(Volume));

            return this;
        }

        #endregion
    }
}
