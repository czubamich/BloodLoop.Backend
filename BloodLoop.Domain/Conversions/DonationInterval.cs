using BloodCore.Domain;
using BloodLoop.Domain.Donations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodLoop.Domain.DonationHelpers
{
    public class DonationInterval : ValueObject
    {
        public string DonationFromLabel { get; set; }
        public virtual DonationType DonationFrom { get; set; }
        public string DonationToLabel { get; set; }
        public virtual DonationType DonationTo { get; set; }
        public TimeSpan Interval { get; set; }

        #region Constructors

        internal DonationInterval() { }

        internal DonationInterval(string donationFromLabel, string donationToLabel, TimeSpan interval)
        {
            DonationFromLabel = donationFromLabel;
            DonationToLabel = donationToLabel;
            Interval = interval;
        }

        internal DonationInterval(DonationType donationFrom, DonationType donationTo, TimeSpan interval)
        {
            DonationFrom = donationFrom;
            DonationFromLabel = donationFrom.Label;
            DonationTo = donationTo;
            DonationToLabel = donationTo.Label;
            Interval = interval;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DonationFromLabel;
            yield return DonationToLabel;
        }

        #endregion
    }
}
