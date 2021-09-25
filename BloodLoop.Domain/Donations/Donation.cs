using Ardalis.GuardClauses;
using BloodCore.Domain;
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

        public DonationType DonationType { get; private set; }

        #region Constructors
        
        private Donation() {}

        private Donation(DonationId id, DonorId donorId, DonationType donationType, DateTime date) : base(id)
        {
            DonorId = Guard.Against.Null(donorId, nameof(Id));
            DonationType = Guard.Against.Null(donationType, nameof(Id));
            Date = Guard.Against.OutOfSQLDateRange(date, nameof(Date));
        }

        #endregion

        #region Creations

        public static Donation Create(DonationId id, DonorId donorId, DonationType donationType, DateTime date)
            => new Donation(id, donorId, donationType, date);

        public static Donation Create(DonorId donorId, DonationType donationType, DateTime date)
            => new Donation(DonationId.New, donorId, donationType, date);

        #endregion

        #region Behaviours

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
            Volume = Guard.Against.NegativeOrZero(volume, nameof(Volume));

            return this;
        }

        #endregion
    }
}
